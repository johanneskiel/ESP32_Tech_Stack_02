# ESP32_Tech_Stack_02


In this tech-stack, you'll learn how to build a robust interface between microcontrollers and Unity, enabling you to integrate sensors of any kind into the game engine. The workshop culminates in transforming an entire classroom of sensors into a unified digital environment. Easily transferable, easily scalable.

## About Unity

Unity is one of the world's leading real-time 3D development platforms, widely used for creating games, interactive experiences, architectural visualizations, and digital art installations. What makes Unity particularly powerful is its accessible C# scripting system, real-time rendering capabilities, and extensive asset ecosystem.

Unity's game engine architecture treats everything as a GameObject: from 3D models and lights to invisible controllers and data processors. This modular approach makes it ideal for sensor-based interactive installations, where physical world data can be mapped to digital behaviors in real-time.

##### Unity Engine: Platform Considerations

Unity faced significant community backlash in 2023 when it announced a controversial Runtime Fee that would charge developers per game installation. This leading to developer boycotts and mass migration to alternatives like Godot. Though the Runtime Fee was fully canceled in September 2024. These decisions highlight the risks of using proprietary platforms. While Unity dominates the educational and indie development space, it's worth knowing alternatives:

Godot is a fully open-source engine with a permissive MIT license and no revenue share requirements. Its lighter weight makes it ideal for 2D projects and smaller teams, though its 3D capabilities and asset ecosystem are less mature than Unity's.

Unreal Engine offers industry-leading graphics quality and is the standard for AAA game development. However, its C++ programming model and Blueprint visual scripting have a steeper learning curve, and its performance overhead makes it less suitable for rapid prototyping.

## About ESP32

The ESP32 is a cost-effective microcontroller with integrated WiFi and Bluetooth, developed by Espressif Systems. It stands out for its small size, low power consumption, and dual-core architecture that enables parallel processing.

What makes it particularly attractive is the open-source ecosystem: The ESP-IDF (Espressif IoT Development Framework) is freely available, and it can be programmed with Arduino IDE, PlatformIO, or MicroPython. These open development environments and the large community provide many free libraries.

For Digital Art, the ESP32 is ideal because it can control sensors, lights and displays, process audio, and through its wireless capabilities enables interactive, networked installations. Its 30+ GPIO pins provide sufficient connections for complex projects, while the open-source tools allow rapid prototyping.

Besides the ESP32, there are other variants like the smaller ESP8266 (WiFi only) or the more powerful ESP32-S3 with improved AI support. These chips are mounted on various development boards, such as the NodeMCU, Wemos D1 Mini, or the official ESP32 DevKit, each offering different form factors and additional features.

## Unity in Artistic Practice

### The Group and its Veins (2022)

by Johannes Kiel

Real-time pheromone simulation, browser-based: https://johanneskiel.bplaced.net/arteeuropa

### Emergent Biosphere (2021)

by Johannes Kiel

Interactive virtual reality with real-time swarm simulation in an artificial biosphere.

Screen recording while wearing the VR headset: https://vimeo.com/manage/videos/555961589

### I didn't find a rocket that attracts me like you do (2025)

by Sofian Biazzi, Emergent Digital Media class

Interactive audiovisual installation: https://www.sofianbiazzi.com/

Sofian Biazzi: "I didn't find a rocket that attracts me like you do uses the mechanics of game engines to question how digital systems transform trauma into navigable space. Inside the perspective of six-year-old Gen Nakaoka, the protagonist of the Japanese manga Barefoot Gen, the player moves through the virtual space, where field recordings, podcast moderation and fragmented testimonies emerge as spatialized sound."

## ESP32 in Artistic Practice

### Empathy Swarm (2019)

by Katrin Hochschuh & Adam Donovan, ZKM Karlsruhe permanent collection

Video / Documentation: https://hochschuh-donovan.com/portfolio/empathy-swarm


### Viral Wave Operators (2024)   

Cooperation, Tatjana Vall and Johannes Kiel.
Two Wave Operators with water level sensors that communicate via water.

![[24 Tatjana Vall & Johannes Kiel Installation View_Photos by Dirk Tacke.jpg]]


---

## Tech-Stack Learning Objectives

| Experience   | Learning goal                                                                                                                                                  |
| ------------ | -------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Beginner** | Overcoming challenges and hurdles: Learn ESP32 features and programming with Arduino IDE, then integrate sensor data into Unity to create an interactive work. |
| **Advanced** | Consolidate knowledge base: Understand the network communication of ESP32 to build a room-scale sensor network integrated into Unity                           |

---

## Table of Contents

### [0. Setup: ESP Arduino IDE & Unity Engine](#0-setup-esp-arduino-ide--unity-engine)

Installation guide for Arduino IDE, USB drivers, ESP32 board support, and Unity Game Engine on Windows, macOS, and Linux.

### [1. Unity: Interface and C#](#1-unity-interface-and-c)

Unity fundamentals: creating your first project, understanding the interface, and basic C# scripting for game objects.

### [2. ESP32: Arduino IDE and C++](#2-esp32-arduino-ide-and-c)

Serial communication between ESP32 and computer, uploading your first sketch, and monitoring output.

### [3. Sensor: Hardware and Cabling](#3-sensor-hardware-and-cabling)

ESP32 DevKit V1 hardware specifications, GPIO pins, and reading sensor data via serial communication.

### [4. Unity + ESP32 + Sensor: Data Transmission](#4-unity--esp32--sensor-data-transmission)

ESP32 to Unity integration:
- 4_1 Integrating a water level sensor into your Unity project
- 4_2 Working with complex sensors (MPU-6050 accelerometer/gyroscope)
- 4_3 Room-scale sensor networks: Network data transmission with ESP-NOW

### [5. Terminology Guide](#5-terminology-guide)

Glossary of essential terms related to ESP32 hardware, development, data transmission, and Unity concepts.

---

# 0. Setup: ESP Arduino IDE & Unity Engine

**All participants should bring the following:**
- Mouse (for better control in Unity)
- For Mac users: MAC standard USB adapter

All participants should carry out this step in advance. No problem if something doesn't work out: Feel free to contact me.

Setup time: 10 - 15 minutes.

## Windows Setup

### Step 1: Download Arduino IDE

1. Go to: **www.arduino.cc/en/software**
2. Download **"Windows Win 10 and newer, 64 bits"**
3. Install the downloaded file

### Step 2: Install USB Drivers

##### CP2102/CP2104 Driver:

1. Download: https://www.silabs.com/documents/public/software/CP210x_Universal_Windows_Driver.zip
2. Extract and **right-click on silabser.inf** → **"Install"**

##### CH340/CH341 Driver:

1. Download: https://assets.techeia.com/downloads/drivers/ch340-ch341ser-driver-win10-11.zip
2. Extract **CH341SER.EXE** and install (Right-click → Run as Administrator)

### Step 3: Add ESP Support

1. Open Arduino IDE
2. "Tools" → "Board" → "Boards Manager..."
3. Search for "ESP32"
4. Install "ESP32 by Espressif Systems"

If you have trouble finding "ESP32 by Espressif Systems" in the Boards Manager, do the following: (otherwise continue to Step 4)

1. Open Arduino IDE
2. **"Arduino IDE"** → **"Preferences"** → **"Settings"**
3. In **"Additional boards manager URLs"** paste:

```
https://raw.githubusercontent.com/espressif/arduino-esp32/gh-pages/package_esp32_index.json
```

### Step 4: Libraries for ESP in Arduino IDE

**MPU6050_light.h:**
- Open Arduino IDE
- **"Tools"** → **"Manage Libraries..."**
- Search for **"MPU6050 light"**
- Install **"MPU6050_light by rfetick"**

### Step 5: Unity

1. Go to: https://unity.com/download
2. Create an account / Log in
3. Download and install the setup file
4. Open Unity Hub
5. Click "Installs" → "Install Editor" → "Install Unity 6.4" → Ignore "Add Modules" just click "Install."

---

## Mac Setup

### IMPORTANT: Please bring MAC standard USB adapter

### Step 1: Download Arduino IDE

1. Go to: **www.arduino.cc/en/software**
2. Download **MacOS Intel** or **MacOS Apple Silicon**
3. Install in Applications folder

### Step 2: Install USB Driver

For newer Macs, drivers (CP2102/CP2104 Driver and CH340/CH341 Driver) are already included in the OS. If you're running macOS Catalina 10.15 (2019) or later, skip this step. Otherwise, contact me

### Step 3: Add ESP Support

1. Open Arduino IDE
2. "Tools" → "Board" → "Boards Manager..."
3. Search for "ESP32"
4. Install "ESP32 by Espressif Systems"

If you have trouble finding "ESP32 by Espressif Systems" in the Boards Manager, do the following: (otherwise continue to Step 4)

1. Open Arduino IDE
2. **"Arduino IDE"** → **"Preferences"** → **"Settings"**
3. In **"Additional boards manager URLs"** paste:

```
https://raw.githubusercontent.com/espressif/arduino-esp32/gh-pages/package_esp32_index.json
```

### Step 4: Libraries for ESP in Arduino IDE

**MPU6050_light.h:**
- Open Arduino IDE
- **"Tools"** → **"Manage Libraries..."**
- Search for **"MPU6050 light"**
- Install **"MPU6050_light by rfetick"**

### Step 5: Unity

1. Go to: https://unity.com/download
2. Create an account / Log in
3. Download and install the setup file
4. Open Unity Hub
5. Click "Installs" → "Install Editor" → "Install Unity 6.4" → Ignore "Add Modules" just click "Install."

---

## Linux (Ubuntu) Setup

### Step 1: Download Arduino IDE

1. Go to: **www.arduino.cc/en/software**
2. Download **"Linux 64 bits"** AppImage
3. Run the AppImage file

### Step 2: Install USB Drivers

For newer Linux distributions, drivers (CP2102/CP2104 Driver and CH340/CH341 Driver) are already included in the kernel.

### Step 3: Install ESP Boards

1. Open Arduino IDE
2. "Tools" → "Board" → "Boards Manager..."
3. Search for "ESP32"
4. Install "ESP32 by Espressif Systems"

If you have trouble finding "ESP32 by Espressif Systems" in the Boards Manager, do the following: (otherwise continue to Step 4)

1. Open Arduino IDE
2. "File" → "Preferences" → "Settings"
3. In "Additional Boards Manager URLs" paste:

```
https://raw.githubusercontent.com/espressif/arduino-esp32/gh-pages/package_esp32_index.json
```

### Step 4: Libraries for ESP in Arduino IDE

**MPU6050_light.h**
- **"Tools"** → **"Manage Libraries..."**
- Search for **"MPU6050 light"**
- Install **"MPU6050_light by rfetick"**

### Step 5: Unity

1. To add the public signing key, run the following command:

```bash
sudo install -d /etc/apt/keyrings
curl -fsSL https://hub.unity3d.com/linux/keys/public | sudo gpg --dearmor -o /etc/apt/keyrings/unityhub.gpg
```

2. To add the Unity Hub repository (x86_64/amd64 only) you need an entry in `/etc/apt/sources.list.d`. Run the following command to add the Unity Hub repository:

```bash
echo "deb [arch=amd64 signed-by=/etc/apt/keyrings/unityhub.gpg] https://hub.unity3d.com/linux/repos/deb stable main" | sudo tee /etc/apt/sources.list.d/unityhub.list
```

3. Update the package cache and install the package:

```bash
sudo apt update
sudo apt install unityhub
```

4. Open Unity Hub
5. Create an account / Log in
6. Click "Installs" → "Install Editor" → "Install Unity 6.4" → Ignore "Add Modules" just click "Install."

---

# 1. Unity: Interface and C#

In this chapter, we'll explore Unity's interface and learn the fundamental concepts of working with GameObjects and C# scripting. By the end, you'll have created your first Unity project and used scripts to move, destroy, and spawn objects.

## Hands on: Creating Your First Unity Project

1. **Open Unity Hub**
2. Click **"New Project"** in the upper right
3. Select **"Universal 3D"** template
4. Select **"3D (Built-In Render Pipeline)"** template
5. Name your project: **"Techstack"**
6. Choose a location on your computer
7. Click **"Create Project"**

Unity will take a moment to set up your project. When it opens, you'll see the main Unity interface.


---

## Unity Interface

Unity's interface consists of several key windows that work together:

### Scene View

The **Scene View** is your 3D workspace where you build and arrange your virtual environment. Here you can:

- Navigate with **Right-Click + WASD** (like a first-person game)
- Pan with **Middle Mouse Button**
- Rotate view with **Alt + Left Mouse**
- Focus on an object by selecting it and pressing **F**

### Game View

The **Game View** shows what the player/user sees through the camera. Press the **Play button** at the top to test your project. While in Play mode, any changes you make are temporary and will be lost when you stop. Always exit Play mode before making changes you want to keep.

### Hierarchy

The **Hierarchy** window lists all GameObjects in your current scene. Think of it as the table of contents for your 3D world. Every object in your scene appears here in a tree structure, showing parent-child relationships.

### Inspector

The **Inspector** displays detailed information about the currently selected GameObject:

- Transform (position, rotation, scale)
- Components (scripts, renderers, colliders, etc.)
- Public variables from your C# scripts

### Project

The **Project** window shows all assets in your project: scripts, materials, textures, prefabs, etc. This is like your file browser within Unity.

### Console

The **Console** displays messages, warnings, and errors. This is essential for debugging.

---

## Behavior of GameObjects

### GameObjects

In Unity, everything is a **GameObject**. Think of it as an empty container or a blank canvas. By itself, a GameObject has no appearance, no behavior, and no functionality: it is simply a named object that exists in your scene at a specific location in 3D space.

- They exist in the scene hierarchy
- They can be parents or children of other GameObjects
- They serve as containers that hold components
- They have a name for identification

### Components

**Components** are modular pieces of functionality that you attach to GameObjects. They define what a GameObject can do, how it looks, how it behaves, and how it interacts with the world. Without components, a GameObject is just an invisible point in space.

Unity follows a **component-based architecture**: instead of creating complex object classes, you build functionality by combining simple, reusable components.

**Example Components:**

| Component         | Description                                                                                                                       |
| ----------------- | --------------------------------------------------------------------------------------------------------------------------------- |
| **Transform**     | Defines position (x, y, z), rotation, and scale. Every GameObject must have this: it's automatically added and cannot be removed. |
| **Mesh Renderer** | Makes the GameObject visible by rendering a 3D mesh with a material (the surface appearance).                                     |
| **Collider**      | Defines invisible physical boundaries for collision detection.                                                                    |
| **Rigidbody**     | Adds physics simulation to the GameObject: gravity, forces, momentum, and realistic collision responses.                          |
| **Scripts (C#)**  | Your custom code that defines unique behaviors and logic.                                                                         |

### Prefabs

**Prefabs** are reusable templates that store complete GameObjects with all their Components. Think of a Prefab as a blueprint: you configure a GameObject once (with all its Components), save it as a Prefab, and then create identical copies whenever you need them.

- They are assets stored in your Project window
- You can instantiate (spawn) multiple copies in your scenes
- Changes to the Prefab automatically update all instances


## Hands on: Understanding Components

1. **Implement Techstack Files **: 
	1. Download the Techstack_Projekt_Folder from this Git repository:  
		1. Click the “Code” drop-down menu in the upper-right corner  
		2. Click “Download ZIP”
	2. Drag and drop the folder ".../Techstack_Projekt_Folder/Unity/Scripts" into "/Assets" of the “Project” window at the bottom of Unity

2. **Create the GameObject:**
   - In the **Hierarchy**, right-click → **3D Object → Sphere**
   - A sphere appears at position (0, 0, 0)
   - This creates a GameObject named "Sphere"

3. **Select the sphere and look at the Inspector**
   - Notice the Components: The GameObject automatically comes with these Components:
     - **Transform**: Position (0, 0, 0), Rotation (0, 0, 0), Scale (1, 1, 1)
     - **Mesh Filter**: Holds the sphere mesh (the 3D geometry)
     - **Mesh Renderer**: Renders the sphere so you can see it
     - **Sphere Collider**: Defines the collision boundary

4. **Add functionality by adding more Components:**
   - Add **Rigidbody** (built-in Component) → press play → Now it responds to gravity and physics
	→ press stop 
   - **Add Component → Move** (Move.cs)
    - Direction: (1, 0, 0)
    - Speed: 10   
	→ press play


---

# 2. ESP32: Arduino IDE and C++

In this chapter, we'll upload our first program to the ESP32 and learn how **Serial Communication** works between the microcontroller and your computer. This communication channel is essential for debugging, monitoring sensor data, and eventually sending data to Unity.

## Arduino IDE and C++

The **Arduino IDE** is a simplified programming environment designed to make microcontroller programming accessible. It provides:

- **Code Editor**: Where you write your programs
- **Compiler**: Converts your code into machine language the ESP32 understands
- **Upload Tool**: Transfers compiled code to the ESP32 via USB
- **Serial Monitor**: Displays real-time text output from your ESP32
- **Library Manager**: Easy access to thousands of pre-written code libraries

**Programming Language:** The Arduino IDE uses a simplified version of **C++**. It handles much of the complexity for you.

**Basic Arduino Sketch Structure:**

```cpp
void setup() {
    // Runs ONCE when the ESP32 starts or resets
}

void loop() {
    // Runs REPEATEDLY after setup() finishes
    // This is your main program: it cycles continuously
}
```

## Buttons on the ESP32 DevKit V1

The ESP32 DevKit V1 has two important buttons:

- **EN Button (Reset)** - Restarts the ESP32 and runs the uploaded sketch (program) from the beginning
- **BOOT Button (Flash/GPIO0)** - Used for uploading sketches (programs). Also programmable as regular input button in your code

**Location:** Both buttons are located on the ESP32 development board next to the USB connector.

## Serial Communication

**Serial Communication** is a method of sending data one bit at a time over a single wire. For the ESP32, this happens over the USB cable, allowing the microcontroller and computer to exchange text messages. The ESP32 uses serial communication at 115200 bits per second (baud rate).

## Hands on: Uploading the First Sketch to ESP32 DevKit

Open **"2_serial_com.ino"** in the Arduino IDE

**Upload process:**

1. **Connect ESP32 via USB** - Use micro-USB cable to connect to computer
2. **Select Board** - In Arduino IDE: "Tools" → "Board" → "ESP32 Dev Module"
3. **Select Port** - "Tools" → "Port" → Choose ESP32 USB port:
   - **Windows:** COM3, COM4, COM5...
   - **Mac:** /dev/cu.usbserial-... or /dev/cu.SLAB_USBtoUART
   - **Linux:** /dev/ttyUSB0, /dev/ttyUSB1...
4. **Click Upload** - Arrow symbol in Arduino IDE or Ctrl+U (Cmd+U on Mac)
5. **Wait** - Sketch compiles: "Connecting..." appears: Press and hold the **BOOT button** for about **2 seconds** during the "Connecting..." phase, then release.

**Open Serial Monitor:**

1. After uploading the sketch: **Open Serial Monitor** - Click magnifying glass icon in Arduino IDE or "Tools" → "Serial Monitor"
2. **Set baud rate** - Select **115200** in dropdown (bottom right of Serial Monitor window)
3. **View output** - Real-time text output from ESP32 appears here
4. **Tip:** If no output appears, press the **EN (Reset) button** on ESP32 to restart.

**What happens in the code:**

The ESP32 starts in the `setup()` function and prepares to send data with `Serial.begin()`. In the `loop()` function, which repeats continuously, it sends data to the computer using `Serial.print()`. To prevent this from happening too quickly, there is a `delay()`. The Serial Monitor can read and display this data.

---

# 3. Sensor: Hardware and Cabling

In this chapter, we'll connect our first sensor to the ESP32 and read real-world data. You'll learn about GPIO pins, how to wire a sensor and how to read sensor values through code. By the end, you'll see live water level readings in the Serial Monitor.

## ESP32 GPIO Pins

**GPIO** stands for **General Purpose Input/Output**. These are the metal pins on your ESP32 that you can configure to either:
- **Input**: Read signals from sensors, buttons, switches
- **Output**: Send signals to LEDs, motors, displays

The **ESP32 DevKit V1** has **30 GPIO pins** available for connecting sensors, actuators, and other electronic components.

- **GND** pin: negative pole
- **3V3** pin: 3.3V output (power for sensors)

![ESP32 DevKit V1](https://m.media-amazon.com/images/I/518GSZDPb6L._AC_.jpg)

## Sensor and Cabling

### Breadboard

Solderless prototyping board with connected holes for temporarily connecting ESP32 pins to components (using jumperkables) .
 
### Water Level Sensor

The water level sensor detects the presence and level of water by measuring conductivity between exposed traces. As water level rises, more traces are connected, creating a variable resistance that outputs an analog voltage signal. **Analog signals** are continuous voltage values between 0V and 3.3V (Digital representation: 4095, 12-bit resolution).

- Operating voltage: 3.3V - 5V
- Output type: Analog (0V - 3.3V)
- Three pins: VCC (power), GND (ground), S (signal)


## Hands on: Water Level Sensor



[![ESP32 DevKit V1 Poti](https://raw.githubusercontent.com/johanneskiel/ESP32_Tech_Stack_01/refs/heads/main/ESP32_poti.png)](https://raw.githubusercontent.com/johanneskiel/ESP32_Tech_Stack_01/refs/heads/main/ESP32_poti.png)
### Wiring:

- **Power off the ESP32** (disconnect USB)
- Insert the ESP32 into the Breadboard as shown in the diagram
- Connect sensor **+ (VCC)** to ESP32 **3V3** < (red wire in the diagram) etc... > 
- Connect sensor **- (GND)** to ESP32 **GND** (black wire in the diagram)
- Connect sensor **S (Signal)** to ESP32 **GPIO 15** ("D15" green wire in the diagram)
- **Double-check all connections** before powering on

### ESP32 Code:

Download and open **"3_WaterSensor.ino"** in the Arduino IDE

**Upload process:**

1. **Connect ESP32 via USB** - Use micro-USB cable to connect to computer
2. **Select Board** - In Arduino IDE: "Tools" → "Board" → "ESP32 Dev Module"
3. **Select Port** - "Tools" → "Port" → Choose ESP32 USB port:
   - **Windows:** COM3, COM4, COM5...
   - **Mac:** /dev/cu.usbserial-... or /dev/cu.SLAB_USBtoUART
   - **Linux:** /dev/ttyUSB0, /dev/ttyUSB1...
4. **Click Upload** - Arrow symbol in Arduino IDE or Ctrl+U (Cmd+U on Mac)
5. **Wait** - Sketch compiles: "Connecting..." appears: Press and hold the **BOOT button** for about **2 seconds** during the "Connecting..." phase, then release.

**Open Serial Monitor:**

1. After uploading the sketch: **Open Serial Monitor** - Click magnifying glass icon in Arduino IDE or "Tools" → "Serial Monitor"
2. **Set baud rate** - Select **115200** in dropdown (bottom right of Serial Monitor window)
3. **View output** - Real-time text output of the water level sensor
4. **→ Test the sensor readings using the water cup**.

**What happens in the code:**

The ESP32 starts in the `setup()` function and prepares the pin to receive data from the sensor. In the `loop()` function, it receives data via `analogRead()` which it sends to the computer using `Serial.print()`. The Serial Monitor can read and display this data.

---

# 4. Unity + ESP32 + Sensor: Data Transmission

In this chapter, we'll integrate physical sensors with Unity. You'll learn to connect water sensors, accelerometers, and build room-scale sensor networks.

## Hands on 4_1: Water Sensor in Unity

Now we'll connect your water sensor to Unity and make GameObjects respond to real-world water levels in real-time.

### ESP32 Code:

We're using the same `3_WaterSensor.ino` from Chapter 3. No need to re-upload.

### Unity Scene:

A Unity scene where GameObjects move up and down based on water level.

##### Ardity (Serial Communication Library)

Unity can't read serial data by default. **Ardity** is a free library that handles ESP32 to Unity communication. 

- Drag and drop the folder ".../Techstack_Projekt_Folder/Unity/Ardity" into "/Assets" of the “Project” window at the bottom of Unity. (Or from: https://ardity.dwilches.com/  and Import into Unity)
- Go to **Edit → Project Settings → Player**:
	- Under **Other Settings**, you'll find **API Compatibility Level**
	- Change it from `.NET Standard 2.0` to **`.NET Framework`** (or `.NET 4.x`)
	- Unity will automatically recompile

##### GameObjects and Scripts:

##### 1. Sphere Prefab

1. **Hierarchy → 3D Object → Sphere** 
   - Scale: (0.3, 0.3, 0.3)
1. **Add Component → Move** (Move.cs)**
   - Direction: (0, 0, 1)
   - Speed: 10
1. **Add Component → Destroy** (Destroy.cs)
   - Max Distance: 10
1. **Drag Sphere to Project window** in "Assets/prefabs" → Creates prefab
2. **Delete Sphere from Hierarchy**

##### 2. Spawner

1. **Hierarchy → Create Empty**
2. Rename: **"Spawner"**
3. Position: (0, 0, 0)
4. **Add Component → Spawn** (Spawn.cs)
   - **Drag Sphere prefab** into Prefab field
   - Time: 0.1
1. **Add Component →  WaveListener** (WaveListener.cs) 
   - Distance: 10

##### 3. SerialController

1. **Hierarchy → Create Empty**
2. Rename: **"SerialController"**
3. **Add Component → Serial Controller** (from Ardity)
4. **Configure Serial Controller:**
   - **Port Name:** (Find your port: Arduino IDE → Tools → Port)
     - Windows: `COM3`, `COM4`, `COM5`...
     - Mac: `/dev/cu.usbserial-XXXX`
     - Linux: `/dev/ttyUSB0`
   - **Baud Rate:** `115200`
1. **Message Listener:**
   - Set messageListener: drag **Spawner** (with the WaveListener Component) from Hierarchy into the Message Listener field

##### Run and Test:

1. **Connect ESP32** via USB (with water sensor wired)
2. **Close Arduino IDE** (Unity needs exclusive port access)
3. **Click Play in Unity**
4. **Check Console:** Should see `"ESP32 Wassersensor connected"`



**What happens in the code:**

SerialController from Ardity
WaveListener.cs: 
Spawn.cs


---

## Hands on 4_2: More Complex Sensors

Now we'll work with the MPU-6050, to create a tilt-controlled platform in Unity.

### MPU-6050 Sensor

The MPU-6050 is a 6-axis accelerometer and gyroscope sensor that measures motion and orientation in 3D space.

**Technical specifications:**
- 3-axis accelerometer (measures acceleration/tilt)
- 3-axis gyroscope (measures rotation)
- I2C communication protocol
- Operating voltage: 3.3V - 5V
- Four pins: VCC (power), GND (ground), SDA (data), SCL (clock)


[![ESP32 DevKit V1 Poti](https://raw.githubusercontent.com/johanneskiel/ESP32_Tech_Stack_01/refs/heads/main/ESP32_poti.png)](https://raw.githubusercontent.com/johanneskiel/ESP32_Tech_Stack_01/refs/heads/main/ESP32_poti.png)
### Wiring:

- **Power off the ESP32** (disconnect USB)
- Connect MPU-6050 **VCC** to ESP32 **3V3** (red wire in the diagram)
- Connect MPU-6050 **GND** to ESP32 **GND** (black wire in the diagram)
- Connect MPU-6050 **SDA** to ESP32 **GPIO 21** (green wire in the diagram)
- Connect MPU-6050 **SCL** to ESP32 **GPIO 22** (purple wire in the diagram)
- **Double-check all connections** before powering on

### ESP32 Code:

Open **"4_2_MPU6050.ino"** in the Arduino IDE and upload it following the same process as before. 

**Upload process:**

1. **Connect ESP32 via USB** - Use micro-USB cable to connect to computer
2. **Select Board** - In Arduino IDE: "Tools" → "Board" → "ESP32 Dev Module"
3. **Select Port** - "Tools" → "Port" → Choose ESP32 USB port:
   - **Windows:** COM3, COM4, COM5...
   - **Mac:** /dev/cu.usbserial-... or /dev/cu.SLAB_USBtoUART
   - **Linux:** /dev/ttyUSB0, /dev/ttyUSB1...
4. **Click Upload** - Arrow symbol in Arduino IDE or Ctrl+U (Cmd+U on Mac)
5. **Wait** - Sketch compiles: "Connecting..." appears: Press and hold the **BOOT button** for about **2 seconds** during the "Connecting..." phase, then release.

**Open Serial Monitor:**

1. After uploading the sketch: **Open Serial Monitor** - Click magnifying glass icon in Arduino IDE or "Tools" → "Serial Monitor"
2. **Set baud rate** - Select **115200** in dropdown (bottom right of Serial Monitor window)
3. **View output** - Real-time text output of the sensor

**What the code does:**

The sketch initializes the MPU-6050 sensor and continuously reads tilt angles in X and Y directions. It sends formatted data (`X:10.5,Y:-5.2`) to Unity via serial communication.

### Unity Scene:

- Keep the existing SerialController in the Hierarchy.
- Delite spawner (in Hierarchy)

##### GameObjects and Scripts:

##### 1. Platform

1. **Hierarchy → 3D Object → Cube**
2. Rename: **"Platform"**
3. Scale: (10, 0.2, 10)
4. Position: (0, 0, 0)
5. **Add Component → Rigidbody
   - is Kinematic: turn on
   - use Gravity: turn off
   - Constraints → Freeze Positions X Y Z: turn on
1. **Add Component → TiltListener** (TiltListener.cs)"
   - Max Tilt Angle: 15
   - Rotation Speed: 5

##### 2. Ball

1. **Hierarchy → 3D Object → Sphere**
2. Rename: **"Ball"**
3. Position: (0, 1, 0)
4. Scale: (0.5, 0.5, 0.5)
5. **Add Component → Rigidbody**
   - Interpolate: Interpolate
   - Collision Detection: Continous 

##### 3. Spawner

1. **Hierarchy → Create Empty**
2. Rename: **"Respawner"**
3. **Add Component → Respawner** (Respawner.cs)

##### 4. SerialController

1. **Hierarchy →**  click **"SerialController"**
2. change **Message Listener:**
   - set Message Listener: Drag **Platform** (with the TiltListener Component) into the Message Listener field



##### Run and Test:

1. **Connect ESP32** via USB (with MPU-6050 wired)
2. **Close Arduino IDE** (Unity needs exclusive port access)
3. Click Play in Unity **and hold the sensor completely still for 2 seconds to allow calibration**
4. **Tilt the MPU-6050** → Platform should tilt, ball should roll

---

## Hands on 4_3: Room-Scale Sensor Networks with ESP-NOW

ESP-NOW is a wireless communication protocol developed by Espressif that allows ESP32 devices to communicate directly with each other without requiring a WiFi router. This is perfect for creating distributed sensor networks.

**ESP-NOW Features:**
- Direct peer-to-peer communication
- Low latency (less than 10ms)
- Low power consumption
- Range up to 200m (line of sight)
- Supports multiple devices (up to 20 peers)

### Architecture:

- **Sender ESP32s**: Multiple ESP32 boards with sensors
- **Receiver ESP32**: Single ESP32 connected to computer, receives all sensor data
- **Unity**: Receives data from receiver ESP32 via serial

### Wiring Sender ESP32:

Each sender ESP32 has the MPU6050 attached.  

### Sender ESP32 Code:

Open **"4_3_esp_now_sender.ino"** in the Arduino IDE and upload it to the sender ESP32.

**Upload process:**

1. **Connect ESP32 via USB** - Use micro-USB cable to connect to computer
2. **Select Board** - In Arduino IDE: "Tools" → "Board" → "ESP32 Dev Module"
3. **Select Port** - "Tools" → "Port" → Choose ESP32 USB port:
   - **Windows:** COM3, COM4, COM5...
   - **Mac:** /dev/cu.usbserial-... or /dev/cu.SLAB_USBtoUART
   - **Linux:** /dev/ttyUSB0, /dev/ttyUSB1...
4. **Click Upload** - Arrow symbol in Arduino IDE or Ctrl+U (Cmd+U on Mac)
5. **Wait** - Sketch compiles: "Connecting..." appears: Press and hold the **BOOT button** for about **2 seconds** during the "Connecting..." phase, then release.

**What it does:** 
1. Reads sensor data
2. Packages sensor x and y with unique device identifier 
3. Broadcasts data to receiver ESP32 via ESP-NOW 
4. Repeats every 100ms for real-time updates 

### Receiver ESP32 Code:

**"4_3_esp_now_receiver.ino"**: We'll configure this together on a shared computer...  

**What it does:**

1. Listens for ESP-NOW messages from senders
2. Forwards received data to Unity via serial

### Unity Scene:

We'll configure this together on a shared computer...  


---

# 5. Terminology Guide

## Unity

| Term           | Explanation                                                                                                            |
| -------------- | ---------------------------------------------------------------------------------------------------------------------- |
| **GameObject** | Fundamental object in Unity scenes. Empty container that holds components to define appearance and behavior.           |
| **Component**  | Modular pieces of functionality attached to GameObjects (Transform, Renderer, Collider, Scripts, etc.).                |
| **Transform**  | Component that defines position, rotation, and scale of a GameObject in 3D space.                                      |
| **Prefab**     | Reusable GameObject template stored as an asset. Changes to prefab update all instances.                               |
| **Hierarchy**  | Window showing all GameObjects in current scene in tree structure.                                                     |
| **Inspector**  | Window displaying properties and components of selected GameObject.                                                    |
| **Scene View** | 3D workspace for building and arranging virtual environment.                                                           |
| **Game View**  | Shows what player sees through camera. Used for testing.                                                               |
| **Console**    | Displays debug messages, warnings, and errors from scripts.                                                            |
| **Script**     | C# code file that defines custom GameObject behavior.                                                                  |
| **Rigidbody**  | Component that enables physics simulation (gravity, forces, collisions).                                               |
| **Collider**   | Component defining invisible boundaries for collision detection.                                                       |
| **Instantiate**| Function to create new GameObject instances at runtime.                                                                |
| **Destroy**    | Function to remove GameObjects from scene.                                                                             |

## ESP32 Hardware & Development

| Term               | Explanation                                                                                                      |
| ------------------ | ---------------------------------------------------------------------------------------------------------------- |
| **IDE**            | Integrated Development Environment - Arduino IDE used for writing and uploading code to ESP32                    |
| **USB Drivers**    | Software (CP2102/CP2104, CH340/CH341) that enables computer communication with ESP32 via USB                     |
| **Serial Monitor** | Arduino IDE tool that displays real-time text output from ESP32 for debugging and monitoring sensor data         |
| **Baud Rate**      | Communication speed (bits per second) between ESP32 and computer - common rates: 9600, 115200 bps                |
| **Library**        | Pre-written code packages (like MPU6050_light.h) that add functionality to ESP32 projects                        |
| **Sketch**         | Arduino program file containing setup() and loop() functions                                                     |
| **Upload**         | Process of transferring compiled code from computer to ESP32                                                     |
| **Compile**        | Converting human-readable code into machine language ESP32 can execute                                           |
| **Breadboard**     | Solderless prototyping board with connected holes for temporarily connecting ESP32 pins to components            |
| **Pin**            | Individual metal contact point on ESP32 that connects to breadboard or components for electrical connection      |
| **GND**            | Ground - Reference voltage point that acts as the negative pole in ESP32 circuits, completes electrical circuits |
| **VCC**            | Voltage Common Collector - General term for positive power supply voltage to components                          |
| **3.3V**           | Three point three volt power supply pin - ESP32's native operating voltage for logic and sensors                 |
| **GPIO Pins**      | General Purpose Input/Output - ESP32's 30+ configurable pins for connecting sensors, LEDs, and other components  |
| **Digital Signal** | Binary electrical signal with only two states: HIGH (3.3V) or LOW (0V)                                           |
| **Analog Signal**  | Continuous voltage signal ranging from 0V to 3.3V, represented digitally as 0-4095                               |
| **I2C**            | Inter-Integrated Circuit - Two-wire communication protocol (SDA/SCL) used by sensors like MPU-6050               |
| **EN Button**      | Reset button on ESP32 DevKit that restarts the microcontroller                                                   |
| **BOOT Button**    | Button used to put ESP32 into upload mode during sketch upload                                                   |

## Data Exchange & Networking

| Term                   | Explanation                                                                                                    |
| ---------------------- | -------------------------------------------------------------------------------------------------------------- |
| **Serial Communication** | Method of sending data one bit at a time over single wire (USB cable between ESP32 and computer)            |
| **ESP-NOW**            | Peer-to-peer wireless communication protocol for direct ESP32-to-ESP32 communication without WiFi router      |
| **MAC Address**        | Unique hardware identifier for ESP32's network interface, required for ESP-NOW pairing                        |
| **Sender**             | ESP32 device that transmits sensor data in ESP-NOW network                                                     |
| **Receiver**           | ESP32 device that receives data from multiple senders and forwards to computer/Unity                           |
| **Ardity**             | Unity library that enables serial communication between Unity and microcontrollers                             |

## Code Concepts

| Term              | Explanation                                                                                          |
| ----------------- | ---------------------------------------------------------------------------------------------------- |
| **Function**      | Named block of code that performs specific task. Can be called/executed from other parts of program  |
| **Variable**      | Named storage location for data that can change during program execution                             |
| **Loop**          | Code structure that repeats execution (Arduino's loop() function runs continuously)                  |
| **Condition**     | Expression that evaluates to true or false, used for decision making in code (if statements)         |
| **Public**        | Variable or function accessible from outside the class/script (visible in Unity Inspector)           |
| **Private**       | Variable or function only accessible within the same class/script                                    |
| **Delta Time**    | Time elapsed since last frame. Used to make movement frame-rate independent (Time.deltaTime)         |
| **Instantiate**   | Creating new instance of object at runtime                                                           |
| **Parse**         | Converting data from one format to another (e.g., string to float)                                   |

