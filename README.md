# RandomRecordsTask
## Short summary
The application generates records, writes them in files, combines these files into one and imports these files into database using db stored procedures.
Records are represented in file in such format:
```
03.03.2015||ZAwRbpGUiK||мДМЮаНкуКД||14152932||7,87742021||
23.01.2015||vgHKThbgrP||ЛДКХысХшЗЦ||35085588||8,49822372||
17.10.2017||AuTVNvaGRB||мЧепрИецрА||34259646||17,7248118||
24.09.2014||ArIAASwOnE||ЧпЙМдШлыфУ||23252734||14,6239438||
16.10.2017||eUkiAhUWmZ||ЗэЖЫзЯШАэШ||27831190||8,10838026||
```
Every record consists of:
- Random date in last 5 years
- 10 random english symbols
- 10 random russian symbols
- Random integer number from 1 to 100.000.000
- Random floating number from 

## Examples
There are some demonstrations of the work.  
Executable code can be found in [Program.cs](https://github.com/Sassiq/RandomRecordsTask/blob/master/RandomRecordsTask.ConsoleApp/Program.cs).  
  
*Generated files with records*  
![image](https://user-images.githubusercontent.com/62505206/197680199-6f47e701-ee6e-40da-a5ab-80b19b491f0c.png)  
  
  
*Content of the generated file*  
![image](https://user-images.githubusercontent.com/62505206/197681253-652cee17-fcdd-4a98-bae6-cad9f797dd95.png)  
  
  
*Combining of files. Combining of files with deletable substring ("abc").*  
![image](https://user-images.githubusercontent.com/62505206/197682437-26be7b0b-130f-45ee-9592-3f800b25e571.png)  
![image](https://user-images.githubusercontent.com/62505206/197682360-adb89f45-ffeb-44ad-909f-63470198f29b.png)  
  
  
*Importing records into database.*  
![image](https://user-images.githubusercontent.com/62505206/197682827-3eaa81e8-1d67-4f64-af2e-a78ef9ebadfc.png)  
![image](https://user-images.githubusercontent.com/62505206/197682913-d79ae2ab-18b7-4c5e-be24-0c840cdd3a9a.png)  
  
  
*Counting sum of integers and median of floating point numbers.*  
![image](https://user-images.githubusercontent.com/62505206/197683130-41942495-6474-4222-9e1b-f5e777151bce.png)  
