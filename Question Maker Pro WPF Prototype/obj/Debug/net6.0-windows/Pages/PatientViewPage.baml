   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages&Question_Maker_Pro_WPF_Prototype.Pages  5      <    6Question_Maker_Pro_WPF_Prototype.Pages.PatientViewPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages     5        N� Title$  PatientViewPage��5	       N� 
Background
 �����.��5      �  � RowDefinitions 5   
   �� 5      $��0.15* �6   �� 5      $
��0.6* �6   .+�5   
   �� $��Patient View��6   $��0��6'   $��Center=�64   $��Center=�6Q   $
��22pt(�6l   $
��Bold"�6|   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $
��0.7* �6#   �� 5      $
��0.4* �6#    5      �� 5      $��0.15* �6    �� 5      $��* �6    .+�5      �� $��Past Reports��6   $��0��6+   $��0��68   $��Center=�6H   $��Center=�6e   $
��Bold"�6�   �� 5      $��Report Fiilter��6   $��0��6-   $��1��6:   $��Center=�6J   $��Center=�6g   $
��Bold"�6�   � 5!      $��1��6    5"      �� 5#      $��0.75* �6$   �� 5$      $��0.25* �6$   .+�5'      &   System.Windows.Controls.DataGrid  -   5          Name$ patientReportDataGrid��5'      $��0��6:   $��0��6G   $��25 10q�6W      
IsReadOnly$
 True��6f      AutoGenerateColumns$ False��6x   "   VerticalScrollBarVisibility$
 auto=�5(       � 	Resources 5*      )  #System.Windows.Controls.DataGridRow	 �� 
TargetType��%E   '        �� 5+      "	  6    .�5,      -   5        5.      5/      
   Columns
 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Last Name��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.8* 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *
lastName+6]     52      $ 
First Name��6-   $
 0.8* 6_    6A   �� *	firstName+6_     53      $ Parent/Teacher��6-   $ * 6i    6E   �� *parentOrTeacher+6i     54      $
 Date��6-   $
 1.2* 6Y    6;   �� *	timestamp+6Y     55      $ Time of Day��6-   $
 0.9* 6`    6B   �� *	timeOfDay+6`   57      A� 59      $��1��6   $
��25 5q�6+   $��Center=�69   $��Center=�6V    A� Rows$ 1��6q    A� Columns$ 4��6z   .+�5:      �� -   5         ��Name$ viewReportButton��5:      $��View Patient Report��67   $��5q�6U   $��5q�6`   6l   �� 5;      -   5        $ questionDataButton��5;      $��View Question Data��69   $��5q�6V   $��5q�6a   6m   �� 5<      -   5        $ 
backButton��5<      $��Go Back��61   $��5q�6C   $��5q�6N   6Z   5=      5>      �� 5@      
�������$��2q�6,   $��1��6@   $��1��6M   $��10 10 25 10q�6]    �� Padding$ 5q�6r   ._�5A      ��  �� Orientation$ Vertical=�6   .+�5B      �� -   5         ��Name$ filterTitleLabel��5B      $��Show reports for:��66   �� 5C      -   5         ��Name$ filterOptionsComboBox��5C      $	��150��6>   $
��Left=�6J   $��5 0 0 15q�6e   5E      �� 5G      $ 
Horizontal=�6!   $��5 0 5 15q�6:   .+�5H      �� -   5         ��Name$ fromDateStackPanel��5H   %   $ Vertical=�6A   $��0 0 25 0q�6X   .+�5I      �� $��From:��6$   (  "System.Windows.Controls.DatePicker  5J      -	   5          Name$ fromDatePicker��5J   )   $	��100��6A   5K      �� 5L      -
   5        $ toDateStackPanel��5L   %   $ Vertical=�6?   .+�5M      �� $	��To:��6$     5N      -   5        $ toDatePicker��5N   )   $	��100��6?   5O      5P      �� 5R      $��Time of Day:��6   $��5 0 0 0q�63   �� 5S      -   5        $ timeOfDayCombobox��5S      $��
10 0 15 15q�6:   6N   �� 5U      $��Parent of Teacher:��6   $��5 0 0 0q�69   �� 5V      -   5        $ parentOrTeacherCombobox��5V      $��
10 0 15 15q�6@   6T   �� 5X      -   5        $ searchButton��5X      $��Search��6H   $
��20 0q�6Y   5Y      5Z      5[      5\      5]      