# DynamicProperty
Change a Property value at run-time without knowing which Property at compile-time
----
## SetPropertyValue()
{code:c#}
void SetPropertyValue(string propertyName, object newValue)
{code:c#}
**Details:**
* Change the Property _propertyName_ of a given Object to the value _newValue_
* Will throw an Exception if the Property is not present
* Will throw an Exception if the Property does not allow writes
**Usage:**
{code:c#}
someObject.SetPropertyValue("Admin", false);
someObject.SetPropertyValue("Name", "Stan");
{code:c#}
----
## SetPropertyValue<T>()
{code:c#}
void GetPropertyValue<T>(string propertyName)
{code:c#}
**Details:**
* Get the Property _propertyName_ of a given Object and cast to T
* Will throw an Exception if the Property is not present
**Usage:**
{code:c#}
bool isAdmin = someObject.GetPropertyValue<bool>("IsAdmin");
string name = someObject.GetPropertyValue<string>("Name");
{code:c#}
----
## SetPropertyValue()
{code:c#}
void GetPropertyValue(string propertyName)
{code:c#}
**Details:**
* Get the Property _propertyName_ of a given Object as an Object
* Will throw an Exception if the Property is not present
**Usage:**
{code:c#}
object isAdmin = someObject.GetPropertyValue("IsAdmin");
object name = someObject.GetPropertyValue("Name");
{code:c#}