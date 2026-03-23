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
<img width="879" height="437" alt="beerTrackerMain" src="https://github.com/user-attachments/assets/32277052-9c13-4c5d-8806-f4d0bc4b1d17" />

Selected beer
<img width="876" height="433" alt="BeerTrackerSelected" src="https://github.com/user-attachments/assets/046f9b69-0527-4072-9d79-63bb430e4ca9" />

Add view
<img width="878" height="433" alt="BeerTrackerAddView" src="https://github.com/user-attachments/assets/44205d0c-cc41-4661-a9c7-671a429d01a0" />

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
