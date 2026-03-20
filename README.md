# BeerTracker 

BeerTracker is a simple beer inventory built with C# and WPF using the MVVM architecture.  
It allows users to track the number of beers in their inventory without manually counting them.

---

## Features

- Add beers to the inventory
- Update or delete items
- Quick quantity adjustments (+1 / -1)
- Custom quantity adjustments
- Restock functionality
- Persistent storage using AppData (JSON)

---

## Technologies

- C#
- WPF
- MVVM architecture
- JSON (AppData persistence)
- JetBrains Rider

---

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern:

- Model: Represents the data structure of beers  
- ViewModel: Handles logic, commands, and state  
- View: WPF UI with data binding  

Commands are implemented to manage user interactions such as adding, updating, and removing items.

---

## Screenshots

Main UI
<img width="879" height="435" alt="beerTrackerMain" src="https://github.com/user-attachments/assets/05ac0be1-ed4d-4095-a5a0-4df14186d78a" />

Selected beer
<img width="881" height="439" alt="BeerTrackerSelected" src="https://github.com/user-attachments/assets/7f527009-c1d7-4d56-a852-3e51229e71ab" />

Add view
<img width="880" height="436" alt="BeerTrackerAddView" src="https://github.com/user-attachments/assets/6985f5c9-145b-48f5-836d-080c1510fbad" />

Modify view
<img width="878" height="434" alt="BeerTrackerModifyView" src="https://github.com/user-attachments/assets/eccfe06e-79aa-45b0-b009-3d6f25b18e29" />

Delete message
<img width="881" height="437" alt="BeerTrackerDeleteMessage" src="https://github.com/user-attachments/assets/34c45b15-5408-462e-9a89-74306841138b" />

Error message
<img width="879" height="437" alt="BeerTrackerError" src="https://github.com/user-attachments/assets/489fd258-b772-4a38-888d-78eace41e8e3" />

Fullscreen
<img width="1915" height="1029" alt="BeerTrackerFullScreen" src="https://github.com/user-attachments/assets/4dadc47d-a953-4a9b-a2f0-6483104ff9b0" />

---

## Motivation

This project started as a simple idea to avoid manually counting beers in my fridge.  
It evolved into a complete desktop application to practice building structured, maintainable software using MVVM.

---

## Future Improvements

- Support for bottle/can images
- Low stock alerts
- Sorting and filtering options
- Potential web version (Vue.js or React)

---

## How to Run

1. Clone the repository
2. Open the solution in JetBrains Rider or Visual Studio
3. Build and run the project
