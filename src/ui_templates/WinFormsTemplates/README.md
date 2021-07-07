# WinForms templates

The idea of this project is templatazing of usage UI.

Even inside into one application we have many similar forms: 
Almost each form is just representation of whole table all few columns from db.
Also it allows user to interact with data and make it convenient: update, add, delete, insert, colorizing, aggregate, so on...

We can create controls and component or event whole forms that will be reused or adopt to many of our applications.

## DataTable

Almost each form in our apps have a table.

Here is the list of functions and states of the table:

- [ ] Read information from db
- [ ] Save information to db (automatically choose update or insert)
- [ ] Data validation
- [ ] Converting of internal field names to human understandable column header with support of switching language
- [ ] Set readonly columns
- [ ] Show/hide selected columns
- [ ] Save state of the table. This means that hided columns names should be save to file settings and load after running.
- [ ] Freeze columns
- [ ] Colorize columns on a different manner (by the unique row value, e.g. container number, detector number, status)
- [ ] Sorting 
- [ ] Searching
- [ ] Filtering

I think the best way to implement this is abstract generic class based on datagridview where the generic type is data model class.

## MessageBox

For report system I can use any of ui wrapper for messages.

Let's start form winform messagebox. We have to prepare message boxes for each level and in case of needed freeze ui thread or not.

- [ ] Error   message (freeze ui thread)
- [ ] Warn    message (can freeze ui thread)
- [ ] Info    message
- [ ] Success message

## Switching language

Applications based on Regata.Core libs have common mechanics for switching language.

In order to switch language we have to add such instructions for each form based on RegisterForm:

~~~csharp
 var rf = new RegisterForm<Measurement>(Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage);
 rf.LangItem.CheckItem(Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage);
 rf.LangItem.CheckedChanged += () => { Settings<MeasurementsSettings>.CurrentSettings.CurrentLanguage = rf.LangItem.CheckedItem; Labels.SetControlsLabels(rf.Controls); };

// in the end of constructor
Labels.SetControlsLabels(rf.Controls); 
~~~

Actually we have a plan to support multiswitching in meaning that switch language in any form of one application lead to automatically switching a language of rest forms. 

> Multiswitching does not work now