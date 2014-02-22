using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


    /// <summary>
    /// This Reader gets and sets values of a given document with a specific format:
    /// key = value0,value1,...,valueN
    /// </summary>
    public class MyReader
    {

        public const String SPACE = "; ";

        String path;
        String[] data;
            
        public MyReader(){}

        public void open(String path)
        {
            this.path = path;
            this.data = File.ReadAllLines(path);
        }

        public void printData()
        {
            if (data == null)
                throw new Exception("no data Declared! Use open()");

            foreach (String s in data)
                System.Console.WriteLine(s);
        }

        public bool isOpen()
        {
            return data != null;
        }

        public void close()
        {
            path = null;
            data = null;
        }

        public String getValue(String key)
        {
            if (data == null)
                throw new Exception("No Data! Use Open before!");

            String searchKey = key + " = ";
            String value = null;

            for (int i = 0; i < data.Length; i++)
            {
                String s = data[i];

                if (s.StartsWith("//") || s.StartsWith("[") || s.Equals(""))
                    continue;

                else if (s.StartsWith(searchKey))
                {
                    String tmp = s.Remove(0, searchKey.Length);
                    value = tmp;
                    break;
                }
            }

            if (value == null)
                throw new Exception(key + " was not found!");

            return value;
        }

        public List<String> getValuesAsList(String key)
        {
            List<String> strings = new List<String>();
            String values = getValue(key);

            String currentString = "";

            for (int i = 0; i < values.Length; i++)
            {
                char c = values.ElementAt(i);

                if (c == ';')
                {
                    strings.Add(currentString);
                    currentString = "";
                    i++;
                    continue;
                }

                else if (i == values.Length - 1)
                {
                    currentString += c;
                    strings.Add(currentString);
                    break;
                }

                else
                {
                    currentString += c;
                }
            }

            return strings;
        }

        public String[] getValuesAsArray(String key)
        {
            List<String> strings = getValuesAsList(key);

            String[] resultStrings = new String[strings.Count];

            int count = -1;
            foreach (String s in strings)
            {
                count++;
                resultStrings[count] = s;
            }

            return resultStrings;
        }

        public void setValue(String key, String value)
        {
            String searchKey = key + " = ";
            bool found = false;

            for (int i = 0; i < data.Length; i++)
            {
                String s = data[i];

                if (s.StartsWith("//") || s.StartsWith("[") || s.Equals(""))
                    continue;

                else if (s.StartsWith(searchKey))
                {
                    data[i] = searchKey + value;
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new Exception("Key wasnt found in setValue(..)");
        }

        public void setValues(String key, String[] values)
        {
            String value = "";

            for (int i = 0; i < values.Length; i++ )
            {
                String s = values[i];

                value = value + s;

                if (i != values.Length - 1)
                {
                    value = value + SPACE;
                }
            }

            setValue(key, value);
        }

        public void setValues(String key, List<String> values)
        {
            String value = "";

            for (int i = 0; i < values.Count; i++)
            {
                String s = values[i];

                value = value + s;

                if (i != values.Count - 1)
                {
                    value = value + SPACE;
                }
            }

            setValue(key, value);
        }

        public void save(String path)
        {

            if (data == null)
                throw new Exception("No data declared! Use .open() before");

            File.WriteAllLines(path, data);
        }

        public void save()
        {
            if(data == null)
                throw new Exception("No data declared! Use .open() before");

            File.WriteAllLines(this.path, data);
        }
    }

