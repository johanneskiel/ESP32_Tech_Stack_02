const int WATER_SENSOR_PIN = 15;  // D15 = GPIO15

void setup() {
  Serial.begin(115200);
  pinMode(WATER_SENSOR_PIN, INPUT);
}

void loop() {
  // Read water level (0-4095 on ESP32)
  float waterLevel = analogRead(WATER_SENSOR_PIN);
  float normalized = waterLevel / 4095.0;

  Serial.println(normalized);
  delay(100);  // 10 updates per second
}