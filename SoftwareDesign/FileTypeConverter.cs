// Code can convert any one of the 4 formats to another so long as they follow valid input syntax (CSV,JSON,MD,HTML)
// to change the table input replace the already existing table files ie (csvfile.CSV,textFile.JSON,mdFile.MD)
// these file names are hardcoded as inputs so keeping the names is vital for functionally howere file content can be changed freely
// so long as it is valid for example textFile.JSON will work if it has valid JSON

//All required input commands are fully functional
//Output is printed into the console
//Using dotnet 6.0, VS code 1.72.0, written on windows 10 and 11

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Program.cs
{ 
    class assignment1{
        public static bool verb = false; //bool to see if verbose is active
        public static string inputfile = "";
        public static void Main(String [] args)
        {
            
            
            string version = "version: 1.0.0";
            string input = Console.ReadLine()!; //read command line input
            string[] inputlist = input.Split(" ");
            string listformats = @"
            Valid Fromats are:

            html
            Json
            MD
            CSV
            ";
            string inputtype =""; //store input and outpur types
            string outputtype ="";
            for(int i = 0; i<inputlist.Length; i++) //for loop to look through inputs
            {
                inputlist[i].Trim();
                if(inputlist[i]=="-i"){
                    Console.WriteLine(version);
                    Console.WriteLine("\n");
                }
                if(inputlist[i]=="-h"){
                    Console.WriteLine(File.ReadAllText("help.txt"));
                    Console.WriteLine("\n");
                }
                if(inputlist[i]=="-l"){
                    Console.WriteLine(listformats);
                    Console.WriteLine("\n");
                }
                if(inputlist[i]=="-v"){
                    verb = true;
                }
                if(inputlist[i].Contains("table")){ //determine if this is either an input or output string
                    if(i>0){ //if its not the first string
                        if(inputlist[i-1]=="-o"){ //if it has -o before it
                            outputtype = inputlist[i].Substring(inputlist[i].IndexOf('.')+1);
                            outputtype = outputtype.ToUpper();
                            outputtype.Trim();
                        }
                        else{ // no -o before ite
                            inputtype = inputlist[i].Substring(inputlist[i].IndexOf('.')+1);
                            inputtype = inputtype.ToUpper();
                            inputtype.Trim();
                        }
                    }
                    else{ // if it is the first string
                        inputtype = inputlist[i].Substring(inputlist[i].IndexOf('.')+1);
                        inputtype = inputtype.ToUpper();
                        inputtype.Trim();
                    }
                }
            }
            if(verb){
                Console.WriteLine("inputs read");
            }
            switch(inputtype){
                case "CSV":
                isCSV(outputtype);
                break;
                case "JSON":
                isJSON(outputtype);
                break;
                case "HTML":
                isHTML(outputtype);
                break;
                case "MD":
                isMD(outputtype);
                break;
            }  
        }
        public static void isJSON(string outputtype){//Input is JSON
            if(verb){
                Console.WriteLine("Converting from JSON");
            }
            string jsonString =(File.ReadAllText("textfile.JSON"));//import file
            List <string> json = (convertJSON(jsonString));//conversion method
            int amountofheaders = int.Parse(json[json.Count()-1]);//retrieve number of headers from list
            string []headers = new string[amountofheaders];
            for(int i = 0; i<amountofheaders; i++){
                headers[i]=json[json.Count()-amountofheaders+i-1];//find headers at the end of list
            }
            json.RemoveAt(json.Count()-1);//remove amount of headers from data
            json.RemoveAll(x => headers.ToList().Exists(y => x.Contains(y)));//remove headers from data
            table middleTable = new table(amountofheaders,json.Count()/amountofheaders+1);//create table rows = headers cols = dataset size
            middleTable.inputdata(headers,amountofheaders,json);//input data into table
            if(verb){
                Console.WriteLine("Deciding output");
                Console.WriteLine("Data Recieved");
            }
            switch(outputtype){ // swtich to determine what type of file to output
                case "CSV":
                middleTable.printCSV();
                break;
            }
            switch(outputtype){
                case "JSON":
                middleTable.printJSON();
                break;
            }
            switch(outputtype){
                case "HTML":
                middleTable.printHTML();
                break;
            }
            switch(outputtype){
                case "MD":
                middleTable.printMD();
                break;
            }
        }
        public static void isHTML(string outputtype){//input is HTML
            if(verb){
                Console.WriteLine("Converting from HTML");
            }
            string htmlString =(File.ReadAllText("htmlfile.HTML"));//import file
            List <string> html = (convertHTML(htmlString));//conversion method
            int amountofheaders = int.Parse(html[html.Count()-1]);//retrieve number of headers from list
            string []headers = new string[amountofheaders];
            for(int i = 0; i<amountofheaders; i++){
                headers[i]=html[i];//find headers at the front of list
            }
            html.RemoveAt(html.Count()-1);//remove amount of headers from data
            html.RemoveAll(x => headers.ToList().Exists(y => x.Contains(y)));//remove headers from data
            table middleTable = new table(amountofheaders,html.Count()/amountofheaders+1);//create table rows = headers cols = dataset size
            middleTable.inputdata(headers,amountofheaders,html);  //input data into table
            if(verb){
                Console.WriteLine("Deciding output");
                Console.WriteLine("Data Recieved");
            }
            switch(outputtype){ // swtich to determine what type of file to output
                case "CSV":
                middleTable.printCSV();
                break;
            }
            switch(outputtype){
                case "JSON":
                middleTable.printJSON();
                break;
            }
            switch(outputtype){
                case "HTML":
                middleTable.printHTML();
                break;
            }
            switch(outputtype){
                case "MD":
                middleTable.printMD();
                break;
            }
        }
        public static void isMD(string outputtype){//input is MD
            if(verb){
                Console.WriteLine("Converting from MD");
            }
            string mdString =(File.ReadAllText("mdfile.MD"));//import file
            List <string> md = (convertMD(mdString));//conversion method
            int amountofheaders = int.Parse(md[md.Count()-1]);//retrieve number of headers from list
            string []headers = new string[amountofheaders];
            for(int i = 0; i<amountofheaders; i++){
                headers[i]=md[i];//find headers at the front of list
            }
            md.RemoveAt(md.Count()-1);//remove amount of headers from data
            md.RemoveAll(x => headers.ToList().Exists(y => x.Contains(y)));//remove headers from data
            table middleTable = new table(amountofheaders,md.Count()/amountofheaders+1);//create table rows = headers cols = dataset size
            middleTable.inputdata(headers,amountofheaders,md);  //input data into table
            if(verb){
                Console.WriteLine("Deciding output");
                Console.WriteLine("Data Recieved");
            }
            switch(outputtype){ // swtich to determine what type of file to output
                case "CSV":
                middleTable.printCSV();
                break;
            }
            switch(outputtype){
                case "JSON":
                middleTable.printJSON();
                break;
            }
            switch(outputtype){
                case "HTML":
                middleTable.printHTML();
                break;
            }
            switch(outputtype){
                case "MD":
                middleTable.printMD();
                break;
            }
        }
        public static void isCSV(string outputtype){ //input is CSV
            if(verb){
                Console.WriteLine("Converting from CSV");
            }
            string csvString =(File.ReadAllText("csvfile.CSV")); //import file
            List <string> csv = (convertCSV(csvString)); //conversion method
            int amountofheaders = int.Parse(csv[csv.Count()-1]); //retrieve number of headers from list
            string []headers = new string[amountofheaders];
            for(int i = 0; i<amountofheaders; i++){
                headers[i]=csv[i]; //find headers at the front of list
            }
            csv.RemoveAt(csv.Count()-1); //remove amount of headers from data
            csv.RemoveAll(x => headers.ToList().Exists(y => x.Contains(y))); //remove headers from data
            table middleTable = new table(amountofheaders,csv.Count()/amountofheaders+1); //create table rows = headers cols = dataset size
            middleTable.inputdata(headers,amountofheaders,csv);  //input data into table
            if(verb){
                Console.WriteLine("Data Recieved");
                Console.WriteLine("Deciding output");
            }
            switch(outputtype){ // swtich to determine what type of file to output
                case "CSV":
                middleTable.printCSV();
                break;
            }
            switch(outputtype){
                case "JSON":
                middleTable.printJSON();
                break;
            }
            switch(outputtype){
                case "HTML":
                middleTable.printHTML();
                break;
            }
            switch(outputtype){
                case "MD":
                middleTable.printMD();
                break;
            }     
        }
        public static List<string> convertJSON(string input){ //method to convert JSON to a list containing pure data
            //break input string up
            List<string> splitted = Regex.Split(input,"\n").ToList();
            //remove excess spaces
            for(int i = 0; i<splitted.Count(); i++){
                splitted[i] = splitted[i].Trim();
            }
            //find number of headers
            int amountofheaders=splitted.FindIndex(a => a == "},")-splitted.FindIndex(a => a == "{")-1;   
            List<string> unwanted = new List<string>(){"[","]","{","}"};
            //remove formatting
            splitted.RemoveAll(x => unwanted.Exists(y => x.Contains(y)));
            string str = String.Join("\n",splitted);
            Char[] delimiters = {':', '\n'};
            List<string> justdata = str.Split(delimiters).ToList();
            string[] headers = new string [amountofheaders];
            for(int i = 0; i<amountofheaders; i++)
            {//find and store headers
                headers[i]=splitted[i].Substring(0,splitted[i].IndexOf(':'));
            }
            //remove headers from data set
            justdata.RemoveAll(x => headers.ToList().Exists(y => x.Contains(y)));
            for(int i = 0; i < headers.Length; i++){
                justdata.Add(headers[i]);
            }//re add headers
            for(int i = 0; i< justdata.Count(); i++){
                justdata[i] = String.Join("", justdata[i].Split(','));
                justdata[i] = String.Join("", justdata[i].Split('"'));
                justdata[i] = String.Join("", justdata[i].Split('”'));
            }//remove commas and quatations
            justdata.Add(amountofheaders.ToString());//add amount of headers at end of list
            justdata = justdata.Select(t => t.Trim()).ToList();
            if(verb){
                Console.WriteLine("Conversion to pure data succesful");
            } 
            return justdata;//return data,number of headers and headers themselves
        }
        public static List<string> convertCSV(string input){ //method to convert CSV to Pure data
            List<string> splitted = Regex.Split(input,"\n").ToList(); //split elements on line break
            List<string> resplit = new List<string>();
            string headers = splitted[0];
            string [] countheaders = headers.Split(","); //count amount of headers by checking number of commas
            int amountofheaders = countheaders.Length;
            for(int i = 0; i<splitted.Count; i++){ //for loop to split elements 0n commas and put them into a new list
                string[] breaker = splitted[i].Split(",");
                for(int j = 0; j<breaker.Length; j++){
                    resplit.Add(breaker[j]);
                }
            }
            for(int i = 0; i< resplit.Count(); i++){
                resplit[i] = String.Join("", resplit[i].Split(','));
                resplit[i] = String.Join("", resplit[i].Split('"'));
                resplit[i] = String.Join("", resplit[i].Split('”'));
            }//remove commas and quatations
            resplit.Add(amountofheaders.ToString()); //add amount of headers to end of list
            if(verb){
                Console.WriteLine("Conversion to pure data succesful");
            } 
            return resplit; //return data
        }
        public static List<string> convertMD(string input){//method to convert MD to Pure data
            List<string> splitted = Regex.Split(input,"\n").ToList(); //split list on new lines
            List<string> resplit = new List<string>();
            splitted.RemoveAt(1); //remove line of dashes -----|---|----
            string headers = splitted[0];
            string [] countheaders = headers.Split("|"); //count amount of headers
            int amountofheaders = countheaders.Length-2;// account for the extra "|" on either end
            for(int i = 0; i<splitted.Count; i++){
                string[] breaker = splitted[i].Split("|"); //break the input up on "|" and add to new list
                for(int j = 0; j<breaker.Length; j++){
                    resplit.Add(breaker[j]);
                }
            }
            resplit = resplit.Select(t => Regex.Replace(t, @"\s+", " ")).ToList(); //remove groups of spaces and replace with a single space
            resplit = resplit.Select(t => t.Trim()).ToList(); // trim all elements
            resplit.RemoveAll(x => string.IsNullOrEmpty(x));  // remove blank or emply elements from the liost
            resplit.Add(amountofheaders.ToString());   // add number of headers to end of list
            if(verb){
                Console.WriteLine("Conversion to pure data succesful");
            }    
            return resplit; //return data
        }
        public static List<string> convertHTML(string input){//method to convert HTML to Pure data
            List<string> splitted = Regex.Split(input,"\n").ToList(); //split on new line
            splitted = splitted.Select(t => t.Trim()).ToList();//trim all elements from the list
            int amountofheaders=splitted.FindIndex(a => a == "</tr>")-splitted.FindIndex(a => a == "<tr>")-1;
            //find the amount of headers by findind the indexs of the first tr and /tr, getting their distance and adjusting by one to find number of headers
            for(int i = 0; i<splitted.Count(); i++){
                if(splitted[i].Contains('>')){
                    splitted[i] = splitted[i].Substring(splitted[i].IndexOf('>')+1);
                } //remove formatting by removing everything inside of < and >
                if(splitted[i].Contains('<')){
                    splitted[i] = splitted[i].Remove(splitted[i].IndexOf('<'));
                }
            }   
            splitted = splitted.Select(t => Regex.Replace(t, @"\s+", " ")).ToList();  //remove groups of spaces and replace with single space
            splitted.RemoveAll(x => string.IsNullOrEmpty(x));  //remove null or empty elements from list
            splitted.Add(amountofheaders.ToString()); // add amount of headers to end of list
            if(verb){
                Console.WriteLine("Conversion to pure data succesful");
            }  
            return splitted; //return data
        }
    }
     
    class table{ // table class contains raw data and also methods to format output
        public int numRows{get;}
        public int numCols{get;}
        public string[][] myTable;

        public table(int numRows, int numCols){ //table constructer to generate table
            this.numRows=numRows;
            this.numCols=numCols;
            this.myTable=new string [numRows][];
            for (int i = 0;i < myTable.Length;i++) // using Length rather than numRows)
            {
                myTable[i] = new string[numCols];
            }
        }
        public void inputdata(string[]headers,int amount,List<string>data){ //method to input data into the table
            for(int i = 0; i <amount; i++){
                myTable[i][0] = headers[i]; //first input headers
            }
            for(int j=0; j<amount; j++){
                int slotfilled = 1; //staring data at index 1 as headers are index 0
                for(int i = 0; i<data.Count(); i++){
                    if(i%amount-j==0){ //distribute data into the correct row by checking if its a multiple of the index of its header
                        myTable[j][slotfilled]=data[i];
                        slotfilled++;
                    }
                }
            }
        }
        public void ShowTable() //method to display table, coppied from demonstration file on moodle
        {
            Console.WriteLine("\nShowTable(): \n\n");
            for (int i = 0; i < this.numRows; i++)
            {
                for (int j = 0; j < this.numCols; j++)
                {
                    Console
                        .WriteLine("myTable[{0}][{1}] = {2}",
                        i,
                        j,
                        this.myTable[i][j]);
                }
            }
            Console.WriteLine("\n");
        }
        public void printCSV() //method to print data in CSV formate
        {
            Console.WriteLine("\nCSV: \n\n"); //title
            for(int i = 0; i<this.numRows; i++)
            {
                for(int j = 1; j<this.numCols;j++){
                    if(!myTable[i][j].Any(char.IsDigit)){ //if data is not a number
                        myTable[i][j]="\""+myTable[i][j]+"\""; //add quatation marks
                    }
                }
            }
            for(int j = 0; j<this.numCols; j++){
                for(int i = 0; i<this.numRows; i++){
                    Console.Write(myTable[i][j]); //output data in columns
                    if(i<this.numRows-1){ // if it is not final string in row
                        Console.Write(","); //add a comma
                    }
                }
                Console.Write("\n"); //new line
            }
            Console.WriteLine("\n"); //new line
        }
        public void printMD() //Method to print data in MD format
        {
            Console.WriteLine("\nMD: \n\n"); //title
            for(int j = 0; j<this.numCols; j++){
                for(int i = 0; i<this.numRows; i++){
                    Console.Write("|"); //start with a |
                    Console.Write(myTable[i][j]); //print data
                }
                Console.Write("|"); //finish row with missing |
                Console.Write("\n");
                if(j ==0){
                    Console.Write("|---------------------------|------------------|-------------------------|"); //write this on second line
                    Console.Write("\n");
                }
            }
            Console.WriteLine("\n");
        }
        public void printJSON() //method to print data in Json format
        {
            Console.WriteLine("\nJSON: \n\n"); //title
            for(int i = 0; i<this.numRows; i++)
            {
                for(int j = 1; j<this.numCols;j++){
                    if(!myTable[i][j].Any(char.IsDigit)){ //if data isnt a number
                        myTable[i][j]="\""+myTable[i][j]+"\""; //add quotations
                    }
                }
            }
            Console.Write("[ \n"); //write opening [
            for(int i = 1; i<numCols; i++){
                Console.WriteLine("{");//{ at the start of each section of data
                for(int j = 0; j<numRows; j++){
                    Console.Write(myTable[j][0]+": "+myTable[j][i]); //write title + : + data
                    if(j<numRows-1){ //there is more lines in this set of data
                        Console.Write(","); //add comma
                    }
                    Console.Write("\n");
                    
                }
                if(i==numCols-1){ //if this is the final piece of data
                    Console.WriteLine("}");
                }
                else{ //it isnt final piece of data so comma is needed
                     Console.WriteLine("},");
                }
            }
            Console.WriteLine("]"); //write closing ]
        }
        public void printHTML(){// method to print data in HTML format
            Console.WriteLine("\nHTML: \n\n"); //title
            Console.WriteLine("<table>"); //opening formatting
            Console.WriteLine("<tr>");
            for(int i = 0; i<numRows; i++){
                Console.WriteLine("<th>"+myTable[i][0]+"</th>"); //write headers
            }
            Console.WriteLine("</tr>");
            for(int i = 1; i<numCols; i++){
                Console.WriteLine("<tr>");
                for(int j = 0;j<numRows; j++){
                    Console.WriteLine("<td>"+myTable[j][i]+"</td>"); //write in html format
                }
                Console.WriteLine("</tr>");
            }
            Console.WriteLine("/<table>"); //closing formatting
        }
    }
}
