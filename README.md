![logo](https://neverway.github.io/files/4_software/id000-nupdk/banner.png?raw=1)

# [007] Neverway Unity Package

The Neverway Unity Project Development Kit, or NUPDK for short, is a Unity package that contains everything you may need for getting started with creating a Neverway project in Unity 2019 or later! NUPDK acts as an excellent starting point for any project from mobile to virtual reality.

Find out more by viewing the project on my website!
<br>https://neverway.github.io/files/4_software/id000-nupdk/software.html


# Installation

### 1.) Make sure all instances of Unity and Unity Hub are closed

### 2.) Download the latest release from https://github.com/Neverway/007-NUP/releases

### 3.) Locate your Unity editor install
On Linux the default path should be `/home/[YOUR USERNAME]/Unity/Hub/Editor/[YOUR UNITY VERSION]`
<br>On Windows the default path should be `C:\Program Files\Unity\Hub\Editor\[YOUR UNITY VERSION]`

The current build is ment to be mounted on top of Unity 2021.3.11f1, which you can install here: 
<br>https://unity3d.com/get-unity/download/archive
<br>But it may work with slightly older versions as well

### 4.) Navigate to the 'Data' subfolder
From the editor folder go to `./Editor/Data` and you should see a folder labeled 'Resources'

### 5.) Extract the archive that you downloaded from step 2
The archive is stored in the format of .tar.gz.
<br>On Windows you will need a program like 7-Zip to extract the files.
<br>7-Zip can be downloaded from here: https://www.7-zip.org/
<br>If when you extract the archive, you get another archive of .gz, just extract that one as well.

### 6.) Copy the 'Resources' folder over to your Unity's 'Data' folder
Inside the archive should be a folder called 'Resources'.
<br>Copy this over to your Unity's 'Data' folder you navigated to in step 4
<br>It should ask you if you want to replace files, and if so, select yes.

### Congratulations!
You should now have the latest version of NUP installed for that version of unity!
<br>When creating a new project, under the 'Core' category, you should see a template labeled 'Neverway'
