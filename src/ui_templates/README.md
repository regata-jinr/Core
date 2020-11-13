# REGATA UI Templates

The idea of code in this repo is implement the set of controls or even forms that could be used in WinForms apps.

In case of think about which linked parts from our UI repeated many times easy to see many common parts that have different implementations but the same purposes.

E.g. often we need to show data base table in dadagrid view but we have to view not whole data base table
but only special columns in one case - developer have to hide some columns in the other - user may want to hide some.

This behaviour obviously should be common for all forms that contain data grid views.

The solution could be the predefined form with submenu like on the picture bellow:

![1](https://sun9-39.userapi.com/c857320/v857320721/158de9/DG7bn3jYl_k.jpg)

Also as you can see on the picture - chosen columns for displaying should be save to settings and keep after application restart.
Usage of settings should be also implemented in common UI mechanics.

This is not an attempt to create new UI engine based on WinForms.
This is an attempt to simplify creation and maintance of our application such as

New data base client
Measurements.Desktop
GammaSpectrumInfo
SampleWeighting
and others old or new
