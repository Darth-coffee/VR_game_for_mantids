# VR_game_for_mantids - Simulating motion parallax

## Introduction

Mantises are among the few invertebrates known to possess stereoscopic vision. However, unlike human binocular vision, mantises rely heavily on motion parallax and a unique behavior called peering to judge depth. Peering involves the mantis rhythmically swaying its body side-to-side, which causes nearby and distant objects to shift against one another in the visual field. This relative movement provides essential depth cues that help the insect estimate the distance to prey or obstacles.

## Project Overview

This project implements a closed-loop virtual reality (VR) system that simulates motion parallax at multiple object distances for freely moving mantises. Here the mantis is untethered, placed at the center of a 360° immersive display formed by three monitors arranged in a triangular configuration.

A camera continuously tracks the mantis’ head movements in real time. Based on this input:

- The field of view of the monitors dynamically updates, shrinking or stretching as the mantis moves.

- Objects within the scene exhibit motion parallax consistent with their simulated distance.

- The mantis experiences a naturalistic, interactive visual world where depth cues adapt to its own movements.

## Repository Structure

1. Closed Loop_Track head, stream and save.ipynb
Tracks the mantis’ head position in real time, streams coordinates, and saves tracking data.

2. Code to record at 60 fps using PG.ipynb
High-speed head tracking using the Point Grey camera (60 fps recording).

### 3. 📂 Unity_scripts (VR Environment)

#### Core Scripts

- BlobDataReciever.cs — Receives live head coordinates streamed from the notebook.

- FirstPersonController.cs — Moves the “player” in the VR world based on head movement, ensuring realistic motion parallax.

- New_FOV1.cs, New_FOV2.cs, New_FOV3.cs — Adjust field of view on each of the three monitors dynamically, creating a consistent immersive view.

#### Utility & Additional Scripts

- Scripts for resetting or exiting the VR game.

- Scripts for multi-screen activation and rendering across all three monitors.

- Scripts for object motion (e.g., oscillating stimuli), useful for experimental manipulations.

This setup enables experiments that closely approximate real-world perception, while maintaining the control and reproducibility of a VR environment.
