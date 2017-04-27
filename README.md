# Naspinski.Utilities

Utilities class for .Net including Dynamic property getters/setters, automatic IQueryable searching, LinqToSql shortcuts, FileStream shortcuts, String extensions and more.

## Getting Started
Naspinski.Utilities is now available via [NuGet](http://nuget.org/packages/Naspinski.Utilities)
Open your Package Manager and search for naspinski.utilities or type the following in the command line:
```
 PM> Install-Package Naspinski.Utilities
```

## Utilities

### [DynamicProperty](https://github.com/naspinski/utilities/blob/master/README.md#dynamicproperty-1)
Change a property value at run time without knowing the property ahead of time

### [EnumUtilities](https://github.com/naspinski/utilities/blob/master/README.md#enumutilities)
Enumerate through enums easily

### IQueryableSearch
Search all/any properties of an IQueryable with one single search.

The 'quick search' is a great tool. Google has shown us that searching with one single, universal search field is the way that people prefer to search. Anyone who has worked with Linq for any amount of time knows this is possible, but it requires a lot of hard-coding and a long jumble of 'where' statements. This class will allow you to run a universal 'google-like' search on any IQueryable.

### LinqToSql [Deprecated]
Universal Get Extensions for your DataContexts, Find the Primary Key of any table, and more

### [FileStreamSave](https://github.com/naspinski/utilities/blob/master/README.md#filestreamsave-1)
Simple extension to save a FileStream to disk, option to avoid overwriting will automatically update the filename to avoid overwriting

### StringConversions
Convert strings to things you often need to convert them to... easily
* *RemoveCharacters* removes any instances of a character array
* *Strings.Random* creates a random string with lower and upper limits of special characters
* *To* turns a string into any Type
* *ToEnum* turns a string into an Enum... simple!
* *ToNullable* turns a string into any Nullable Type you want

# Documentation
## DynamicProperty
Change a Property value at run-time without knowing which Property at compile-time

### SetPropertyValue(string, object)
```
void SetPropertyValue(string propertyName, object newValue)
```
*Details:*
* Change the Property *propertyName* of a given Object to the value *newValue*
* Will throw an Exception if the Property is not present
* Will throw an Exception if the Property does not allow writes
*Usage:*
```
someObject.SetPropertyValue("Admin", false);
someObject.SetPropertyValue("Name", "Stan");
```

### GetPropertyValue&lt;T&gt;(string)
```
void GetPropertyValue<T>(string propertyName)
```
*Details:*
* Get the Property *propertyName* of a given Object and cast to T
* Will throw an Exception if the Property is not present
*Usage:*
```
bool isAdmin = someObject.GetPropertyValue<bool>("IsAdmin");
string name = someObject.GetPropertyValue<string>("Name");
```
### GetPropertyValue(string)
```
void GetPropertyValue(string propertyName)
```
*Details:*
* Get the Property *propertyName* of a given Object as an Object
* Will throw an Exception if the Property is not present
*Usage:*
```
object isAdmin = someObject.GetPropertyValue("IsAdmin");
object name = someObject.GetPropertyValue("Name");
```

## EnumUtilities
Simple Enum utilities

### GetValues&lt;T&gt;()

*Details:*
* Gets a collection of enums (useful for looping)
* ```T : enum```

*Usage:*
```
enum Tester { Test = 1, Bla = 5 }

// loop through them
foreach(var e in EnumUtilities.GetValues<Tester>())
    System.PrintLine(e.ToString() + " = " + (int)e);
```

## FileStreamSave
Extension for simply saving a FileStream

### Save()
```
string Save(string path, (optional)bool overwrite)
```

*Details:*
* Saves FileStream to *path* then returns the saved file name
* *overwrite* defaults to **false** if it is not specified
* if *overwrite* is **false** or omitted and the file already exists, it will append a counter to the file name; **file.txt**, **file[1].txt**, **file[2].txt**, etc.
* if *overwrite* is **true** it will overwrite any existing files
* if the folder you are trying to write to does not exist, it will be automatically made

*Usage:*
```
//getting a FileStream
FileStream stream = File.OpenRead(@"C:\file.txt");

//save it to a new directory
string fileName = stream.Save(@"C:\new_directory\file.txt");
//fileName now equals 'file.txt'

//save it again
string fileName = stream.Save(@"C:\new_directory\file.txt");
//fileName now equals 'file[1].txt' as overwrite defaults to false
//now there are 2 files in that folder: file.txt & file[1].txt

//overwrite the original file
stream.Save(@"C:\new_directory\file.txt", true);
```

## [StringConversions](https://github.com/naspinski/utilities/blob/master/README.md#stringconversions-1)
Turns string into other useful stuff without all the code

### RemoveCharacters()
```
string RemoveCharacters(char[] characters)
```

*Details:*
* Removes all instances of provided characters from the string

*Usage:*
```
string s = "abcdefgaaaaa";

//remove some characters
s = s.RemoveCharacters(new[] { 'a', 'b' });

// s is now "cdefg" 
```
 
### SplitCamelCase()

```
string SplitCamelCase()
```

*Details:*
* Splits a CamelCase string into a human readable string
* Deals with Acronyms pretty well

*Usage:*
```
string s = "StanRulesTheUSA";
string sReadable = s.SplitCamelCase();

// sReadable is now "Stan Rules The USA"
```

### ToEnum&lt;T&gt;()
```
T ToEnum<T>()
```

*Details:*
* Attempts to convert input string into an enum of Type *T*
* If string is not a match for the enum *T*, a *ArgumentException* is thrown
* If conversion fails, it will try to capitalize the fist letter to go with .Net naming conventions, i.e.: "abc" -> "Abc"

*Usage:*
```
//showing what the enum looks like:
public enum Cars { "Corvette", "Pinto" };

//convert a string to the enum
Cars car = "Corvette".ToEnum<Cars>();

Debug.Assert(car == Cars.Corvette);
```

### Strings.Random(int, *int)

```
string Strings.Random(int length, *int minSpecialCharacters)
```

*Details:*
* Creates a random string
* if *minSpecialCharacters* is not specified, it will produce a random string that is *alphanumeric only*

*Usage:*
```
string s = Strings.Random(10);
// s = mPbsKC968r

s = Strings.Random(10, 3);
// s = 4(8VU_GyS|
// or more special characters like: >=irRG+1a!
```

### ToEnum&lt;T&gt;()
```
T ToEnum<T>()
```

*Details:*
* Attempts to convert input string into an enum of Type *T*
* If string is not a match for the enum *T*, a *ArgumentException* is thrown
* If conversion fails, it will try to capitalize the fist letter to go with .Net naming conventions, i.e.: "abc" -> "Abc"

*Usage:*
```
//showing what the enum looks like:
public enum Cars { "Corvette", "Pinto" };

//convert a string to the enum
Cars car = "Corvette".ToEnum<Cars>();

Debug.Assert(car == Cars.Corvette);
```
### To&lt;T&gt;()
```
T To<T>()
```

*Details:*
* Attempts to convert input into a Type *T*
* *{{ T : struct }}*

*Usage:*
```
double eight = "8".To<double>();
```

### ToNullable&lt;T&gt;()
```
Nullable<T> ToNullable<T>()
```

*Details:*
* Attempts to convert input into a nullable version of Type *T*
* *{{ T : struct }}*

*Usage:*
```
int? eight = "8".ToNullable<int>();
decimal? twelve = "12".ToNullable<decimal>();
double? nullDouble = "".ToNullable<double>(); //outputs null
```
