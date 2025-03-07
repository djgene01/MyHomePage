# My Dashboard

## Dashboard Preview
A customizable, offline-capable personal productivity dashboard built with Blazor.

## Overview
**My Dashboard** is a sleek, browser-based application designed to help you organize your daily life. It features sections for managing bookmarks (with categories), notes, and to-do lists, all stored locally in your browser's local storage for privacy and offline access. Customize your experience with beautiful wallpapers and enjoy a dark/light mode toggle for day or night use.

Built with **Blazor WebAssembly, Tailwind CSS, and DaisyUI**, this project showcases modern web development techniques while keeping user data secure and private.

## Features
- **Bookmarks Manager**: Organize your favorite websites with customizable categories in a clean, scrollable interface.
- **Notes Section**: Jot down quick thoughts or ideas *(implementation pending in child component).*
- **To-Do List**: Track tasks efficiently *(implementation pending in child component).*
- **Customizable Wallpaper**: Choose from a curated set of high-quality Unsplash images to personalize your dashboard.
- **Dark/Light Mode**: Toggle between themes with local storage persistence for a comfortable viewing experience.
- **First-Time User Popup**: A friendly welcome modal explaining how to use the dashboard and where data is stored.
- **Data Management**: Export your data as a JSON file or import existing data with a confirmation prompt.
- **SEO Optimized**: Includes Open Graph and Twitter Card meta tags for beautiful link previews when shared.
- **Offline Support**: Works without an internet connection since all data is stored locally.

## Demo
Try it out live at: [https://myhome.adminsoftware.co.za/](https://myhome.adminsoftware.co.za/)
![image](https://github.com/user-attachments/assets/e52cc835-d210-432c-abdb-5ae6ab24da72)

---

## Installation
To run **My Dashboard** locally, follow these steps:

### Prerequisites
- .NET 8 SDK (or the version your project targets)
- A modern web browser (Chrome, Firefox, Edge, etc.)

### Steps
#### Clone the Repository
```bash
git clone https://github.com/djgene01/my-dashboard.git
cd my-dashboard
```

#### Restore Dependencies
```bash
dotnet restore
```

#### Run the Application
```bash
dotnet run --project MyHomePage
```
*Replace `MyHomePage` with your actual project name if different.*

#### Open in Browser
Navigate to [https://localhost:5001](https://localhost:5001) (or the port shown in your terminal).

---

## Usage
### First Launch
A welcome popup will guide you through the basics.

### Bookmarks
- Add links with categories via the input fields.
- Switch categories using the dropdown.

### Wallpaper
- Click the image icon in the header to select a new background.

### Theme
- Toggle dark/light mode using the sun/moon button next to the settings gear.

### Settings
Click the gear icon to:
- **Export Data**: Download your dashboard data as `dashboard-data.json`.
- **Import Data**: Upload a JSON file to restore your data *(overwrites existing data after confirmation).*
- **Data Storage**: All data (bookmarks, wallpaper choice, theme preference) is saved in your browser’s local storage.

---

## Project Structure
```
my-dashboard/
├── MyHomePage/                # Main Blazor project folder
│   ├── Pages/                 # Page components
│   │   └── Index.razor        # Main dashboard page
│   ├── Components/            # Reusable components (e.g., BookmarksSection)
│   ├── wwwroot/               # Static files
│   │   ├── index.html         # HTML entry point with SEO metadata
│   │   └── css/               # Stylesheets (app.css, etc.)
│   └── Program.cs             # Blazor app entry point
├── README.md                  # This file
└── MyHomePage.csproj          # Project file
```

---

## Customization
- **Wallpapers**: Add more URLs to the `PresetWallpapers` list in `Index.razor`.
- **SEO**: Update meta tags in `wwwroot/index.html` with your domain and preview image.
- **Styling**: Modify Tailwind classes in components or extend `app.css` for custom styles.

---

## Contributing
**Contributions are welcome!** To contribute:
1. Fork the repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature/awesome-feature
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add awesome feature"
   ```
4. Push to the branch:
   ```bash
   git push origin feature/awesome-feature
   ```
5. Open a **Pull Request**.

*Please ensure your code follows the existing style and includes appropriate comments.*

---

## License
This project is licensed under the **MIT License**. See the `LICENSE` file for details.

---

## Acknowledgments
- **Built with ❤️ by Eugene Bosman**
- **Powered by Blazor, Tailwind CSS, and DaisyUI.**
- **Images sourced from Unsplash.**
