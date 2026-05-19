#include <esp_now.h>
#include <WiFi.h>
#include <Wire.h>
#include <MPU6050_light.h>

const int PROJECT_ID = 12345;
const char COLOR[8] = "#0000FF";
/*
black: "#000000",
white: "#FFFFFF",
red: "#FF0000",
green: "#00FF00",
blue: "#0000FF",
yellow: "#FFFF00",
orange: "#FFA500",
purple: "#800080",
pink: "#FFC0CB",
brown: "#A52A2A",
gray: "#808080",
cyan: "#00FFFF",
*/

const unsigned long SEND_INTERVAL = 200;
const int LED_PIN = 2;

uint8_t broadcastAddress[] = {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};

typedef struct __attribute__((packed)) {
  int projectID;
  char color[8];
  float x;
  float y;
} SensorData;

SensorData dataPacket;
MPU6050 mpu(Wire);

unsigned long lastSend = 0;

void setup() {
  Serial.begin(115200);
  delay(1000);

  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, HIGH);

  Wire.begin();
  Wire.setClock(100000);   

  byte status = mpu.begin();
  if (status != 0) {
    Serial.print("MPU6050 Fehler: ");
    Serial.println(status);
    while (true) {
      digitalWrite(LED_PIN, !digitalRead(LED_PIN));
      delay(200);
    }
  }

  Serial.println("Calibrating... Do not move the sensor!");
  delay(2000);
  mpu.calcOffsets(true, true);

  digitalWrite(LED_PIN, LOW);
  Serial.println("MPU ready");

  WiFi.mode(WIFI_STA);
  WiFi.disconnect();
  WiFi.setSleep(false);
  WiFi.setTxPower(WIFI_POWER_2dBm);

  if (esp_now_init() != ESP_OK) {
    Serial.println("ESP-NOW init failed");
    while (true) {
      digitalWrite(LED_PIN, !digitalRead(LED_PIN));
      delay(100);
    }
  }

  esp_now_peer_info_t peerInfo = {};
  memcpy(peerInfo.peer_addr, broadcastAddress, 6);
  peerInfo.channel = 0;
  peerInfo.encrypt = false;

  if (esp_now_add_peer(&peerInfo) != ESP_OK) {
    Serial.println("Failed to add peer");
  }

  dataPacket.projectID = PROJECT_ID;
  strncpy(dataPacket.color, COLOR, sizeof(dataPacket.color) - 1);
  dataPacket.color[sizeof(dataPacket.color) - 1] = '\0';

  Serial.println("ESP-NOW ready");
}

void loop() {
  mpu.update();

  if (millis() - lastSend >= SEND_INTERVAL) {
    lastSend = millis();

    dataPacket.x = mpu.getAngleX();
    dataPacket.y = mpu.getAngleY();

    esp_err_t result = esp_now_send(
      broadcastAddress,
      (uint8_t*)&dataPacket,
      sizeof(dataPacket)
    );

    if (result != ESP_OK) {
      Serial.print("Send error: ");
      Serial.println(result);
    }

    Serial.print("X:");
    Serial.print(dataPacket.x, 1);
    Serial.print(",Y:");
    Serial.println(dataPacket.y, 1);
  }
}