#include <esp_now.h>
#include <WiFi.h>

const int PROJECT_ID = 12345;
const char COLOR[8] = "#FFFF00";
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

const unsigned long SEND_INTERVAL = 200; //50
uint8_t broadcastAddress[] = {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};

typedef struct __attribute__((packed)) {
  int projectID;
  char color[8];
  float x;
  float y;
} SensorData;

SensorData dataPacket;

void setup() {
  Serial.begin(115200);
  WiFi.mode(WIFI_STA);
  
  if (esp_now_init() != ESP_OK) {
    Serial.println("ESP-NOW Init Error");
    return;
  }
  
  esp_now_peer_info_t peerInfo = {};
  memcpy(peerInfo.peer_addr, broadcastAddress, 6);
  peerInfo.channel = 0;
  peerInfo.encrypt = false;
  esp_now_add_peer(&peerInfo);
  
  dataPacket.projectID = PROJECT_ID;
  strncpy(dataPacket.color, COLOR, sizeof(dataPacket.color) - 1);
}

void loop() {
  float sensorValueX = random(0, 100) / 10.0;
  float sensorValueY = random(0, 100) / 10.0;
  dataPacket.x = sensorValueX;
  dataPacket.y = sensorValueY;
  
  esp_now_send(broadcastAddress, (uint8_t*)&dataPacket, sizeof(dataPacket));
  
  Serial.print(dataPacket.color);
  Serial.print(",");
  Serial.print(dataPacket.x);
  Serial.print(",");
  Serial.println(dataPacket.y);
  
  delay(SEND_INTERVAL);
}