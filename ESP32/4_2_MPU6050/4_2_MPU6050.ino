#include <Wire.h>
#include <MPU6050_light.h>

MPU6050 mpu(Wire);

// Onboard LED Pin for ESP32 DEV Kit v1
const int LED_PIN = 2;

void setup() {
  Serial.begin(115200);
  
  Wire.begin();
  mpu.begin();

  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, HIGH);
  Serial.println("Calibrating... Do not move the sensor!");
  delay(2000);
  mpu.calcOffsets(true, true);
  digitalWrite(LED_PIN, LOW);
  Serial.println("Ready!");
}

void loop() {
  // Update sensor
  mpu.update();
  
  // Send angles via Serial
  Serial.print("X:");
  Serial.print(mpu.getAngleX(), 1);
  Serial.print(",Y:");
  Serial.println(mpu.getAngleY(), 1);
  
  delay(20);  // 50 updates per second
}