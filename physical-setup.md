# Building a Unity World Controlled by a freely moving Insect  

In this guide, we'll create a Unity project where a player object is controlled by an insect moving within a triangular arena formed by 3 monitors. The goal is to synchronize a first-person virtual player's movements in Unity with those of the insect's to mimic how the it perceives the real world. We'll be using Python-OpenCV for detection and UDP for communication. Here's what we'll cover:

1. [Setting Up OpenCV](#setting-up-opencv)

1. [Setting Up Unity](#setting-up-unity)

1. [The Physical Setup](#the-physical-setup)




Let's get started! 🚀

## Setting Up OpenCV

### Installation

1. **Install OpenCV and other dependencies**  
     ``` py
     pip install cvzone
     ```

### Python Script to Detect Coordinates and send them via UDP
   - Here's a brief overview of what the script should do:

1. **Detect the Insects's coordinates:**  
   - Import necessary libraries (`cvzone` for easy color detection, `cv2` for OpenCV functions).
   - Set up video capture and define the center of the image for reference.
   - Initialize a color finder. Here you can set thresholds for the detection of the insect/marker/retroreflective substance. (I'll change this once our method is finalized) 
   - Use blob tracking, find the largest blob and get coordinates of its centre. 
   - Adjust the coordinate system so that the centre of the camera's field of view corresponds to (0,0). This ensures that the centre of the arena corresponds to the centre of the world created in Unity. 
   This is considering the camera is poisitioned right above the centre of the arena.
2. **Send the detected coordinates via UDP:**
   - Import `socket` 
   - Use UDP sockets to send the coordinates to the Unity application.
3. **Calibration:**
   - Measure the physical area that the camera will cover in the real world. This is the space where the insect will move.  
    Check the camera resolution and calculate the real-world distance per pixel. Convert the pixel coordinates to the units used in Unity to ensure a 1:1 movement translation from the insect to the player.

 You can check out the script for doing this with a webcam [here]().

_**Note 1**: You can use a different camera or a communication method other than UDP._  
_**Note 2**: This code only sends the x and y coordinates of the insect from a camera fixed above it. You can also send z coordinates by calculating the area of the detected contour._

## Setting Up Unity

### 1. Start a Project
   - Begin by creating a built-in simple 3D project offered by Unity.
### 2. Player Object
   - Create a 3D object and assign it the 'Player' tag.
   - Attach three cameras to this player object. Set their horizontal FOVs to 120° and rotations about the y-axis to 0, 120° and -120°, so they together cover 360°. Adjust the vertical FOV to suit your display's aspect ratio. Each of these cameras will display thier views on one of the monitors.  
   (This object will represent the insect in the virtual world)
### 3. Script for Player translation
   - This script first recieves the Insect's coordinates being sent via UDP. The player object in Unity should move just as the insect does, so this script assigns the insect's coordinates received from python-opencv to those of the player's directly (since there is 1:1 mapping)
   Here's an example [link]()
  
<div style="display: flex; flex-direction: row;">
    <div style="width: 60%;">

   ### 4. Script for changing FOV (field of view) of cameras
   - When the insect is at the center of the arena, the horizontal FOV of each camera is 120°, as shown in the figure by the white dotted lines.    
   - As the insect moves, the cameras must adjust to the changing perspectives of both the virtual and real players. Therefore, the field of view (FOV) displayed on each screen must be dynamically updated (as illustrated in the figure).    
    This can be done with a script that calculates the angles subtended by the player across each screen and update the camera's view. Rotations of the camera also need to be adjusted.

     A script for updating cameras can be found [here]()  
  
</div>
<div style="width: 50%;">
   <img src="FOV_diagram.png" alt="FOV diagram" width="273.35" height="277.55">
   </div>
</div>


### 4. Terrain
   - DIY. Create a terrain 3D object and play around with the available tools.

## The Physical Setup  
   - Arrange 3 monitors in an equilateral triangle.
   - Find the centroid of this triangle and place a platform for the insect
   - Fix a camera above the platform such that the centre of the camera's view corresponds to the position of the platform.  
   - IR lights would be required if retroreflectors are being used.  
<img src="VR_setup.png" alt="setup diagram" width="i92.5" height="205.1">
