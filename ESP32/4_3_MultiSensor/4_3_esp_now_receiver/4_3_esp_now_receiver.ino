#include <esp_now.h>
#include <WiFi.h>

const int PROJECT_ID = 12345;

typedef struct __attribute__((packed)) {
  int projectID;
  char color[8];
  float x;
  float y;
} SensorData;

SensorData receivedData;

void onDataReceived(const esp_now_recv_info *recvInfo, const uint8_t *incomingData, int dataLen) {
  memcpy(&receivedData, incomingData, sizeof(receivedData));
  
  if (receivedData.projectID == PROJECT_ID) {
    // MAC-Adresse des Senders ausgeben (kommt automatisch von ESP-NOW)
    Serial.printf("%02X:%02X:%02X:%02X:%02X:%02X,", 
                  recvInfo->src_addr[0], recvInfo->src_addr[1], 
                  recvInfo->src_addr[2], recvInfo->src_addr[3],
                  recvInfo->src_addr[4], recvInfo->src_addr[5]);
    
    Serial.print(receivedData.color);
    Serial.print(",");
    Serial.print(receivedData.x);
    Serial.print(",");
    Serial.println(receivedData.y);
  }
}

void setup() {
  Serial.begin(115200);
  WiFi.mode(WIFI_STA);
  
  if (esp_now_init() != ESP_OK) {
    Serial.println("ESP-NOW Init Error");
    return;
  }
  
  esp_now_register_recv_cb(onDataReceived);
}

void loop() {
  delay(100);
}