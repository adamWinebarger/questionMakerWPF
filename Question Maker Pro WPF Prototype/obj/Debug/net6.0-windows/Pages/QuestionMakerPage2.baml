   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages&Question_Maker_Pro_WPF_Prototype.Pages  5      ?    9Question_Maker_Pro_WPF_Prototype.Pages.QuestionMakerPage2   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages     5        N� Title$  QuestionMakerPage2��5	      .��5      � ���#e9ecef � RowDefinitions 5   
   �� 5      $��0.25* �6   �� 5      $��0.65* �6   5      �� 5      $��0.25* �6   �� 5      $
��0.1* �6   .+�5   
   �� -   5         ��Name$ 
titleLabel��5      $��0��6$   $��Questionnaire Setup��61   $��Center=�6O   $��Center=�6l   $
��36pt(�6�   $
��Bold"�6�   � 5   
   $��1��6   $
��10 0q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#    5      �� 5      $
��0.1* �6    �� 5      .+�5       �� $��Parent Questionnaire��6   $��0��63   $��0��6@   $��Center=�6P   $��Center=�6m   �� 5!      $��Teacher Questionnaire��6   $��0��64   $��1��6A   $��Center=�6Q   $��Center=�6n   &   System.Windows.Controls.DataGrid  5#      -   5          Name$ parentQuestionsDataGrid��5#      $��0��68   $��1��6H   $��10q�6U      
IsReadOnly$
 True��5$   S   $
��Auto=�6e      AutoGenerateColumns$ False��5%       � 	Resources 5(      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5)      "  6   .�5*      -   5        5-      5.      	   Columns	 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn
   Header$	
 No.��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 0.2* 6Y   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *questionNumber+6Y     53      $
 Question��6)   $ * 6\    6;   �� *questionText+6\   55        57      -   5        $ teacherQuestionsDataGrid��57      $��1��69   $��1��6I   $��10q�6V   $
 True��58   S   $
��Auto=�6e   $ False��59       5;      ��%E   '        �� 5<      "  6   .�5=      -   5        5@      5A      	 5D        5E      $	
 No.��6)   $
 0.2* 6Y    66   �� *questionNumber+6Y     5F      $
 Question��6)   $ * 6\    6;   �� *questionText+6\   5H      5I      A� 5K   
   $��2��6    A� Rows$ 2��6#   $��Center=�6,   $	��Top=�6I   $	��0 5q�6a   .+�5L      �� -   5         ��Name$ addQuestionButton��5L      $��Add Question��60   $��4q�6G   $	��0 5q�6h   �� 5M      -   5        $ editQuestionButton��5M      $��Edit Question��61   $��4q�6I   $	��0 5q�6j   �� 5N      -   5        $ removeQuestionButton��5N      $��Remove Question��63   $��4q�6M   $	��0 5q�6n   �� 5O      -	   5        $ resetQuestionnaireButton��5O      $��Reset Questionnaire��67   $��4q�6U   $	��0 5q�6v   �� 5P      -
   5        $ saveQuestionnaireButton��5P      $��Save Questionnaire��66   $��4q�6S   $	��0 5q�6t   �� 5Q      -   5        $ moveUpButton��5Q      $��Move Up��6+   $
��10 4q�6=   $	��0 5q�6a   �� 5R      -   5        $ moveDownButton��5R      $��	Move Down��6-   $��4q�6A   $	��0 5q�6b   �� 5S      -   5        $ submitButton��5S      $��Finish��6+   $�� 20 4q�6<   $	��0 5q�6a   5U      5W      5X      