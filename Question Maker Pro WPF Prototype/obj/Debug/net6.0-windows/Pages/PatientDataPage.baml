   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages&Question_Maker_Pro_WPF_Prototype.Pages  5      <    6Question_Maker_Pro_WPF_Prototype.Pages.PatientDataPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages     5        N� Title$  PatientDataPage��5	      .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $
��0.5* �6   �� 5      $��0.25* �6   .+�5   
   �� $��0��6   $��Patient View��6   $��Center=�64   $��Center=�6Q   $
��30pt(�6l   $
��Bold"�6|   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ patientDataGrid��5      $��1��6,   $��25q�6:      
IsReadOnly$
 True��6z      AutoGenerateColumns$ False��5      "   VerticalScrollBarVisibility$
 Auto=�6/   $��Stretch=�6R    � 	Resources 5      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5      "  6   .�5      -   5        5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
First Name��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	firstname+6W     5      $	 	Last Name��6%   $
 * 6U    68   �� *
lastname+6U     5      $		 Age��6%   $

 0.5* 6J    62   �� *age+6J     5       $	 Date of Birth��6%   $
 0.75* 6\    6<   �� *dateOfBirth+6\     5!      $	 Gender��6%   $

 0.7* 6P    65   �� *gender+6P     5"      $	 Parent Code��6%   $
 * 6Y    6:   �� *
parentCode+6Y     5#      $	 Teacher Code��6%   $
 * 6[    6;   �� *teacherCode+6[   5%      A� 5(   
   $��2��6    A� Rows$ 2��6#    A� Columns$ 4��6,   $��Center=�68   $��Center=�6U   .+�5)      �� -   5         ��Name$ viewPatientButton��5)      $��View Patient��60   $	��5 1q�6G   $	��5 5q�6U   6b   �� 5*      -   5        $ copyParentCodeButton��5*      $��Copy Parent Code��63   $	��5 1q�6N   $��5q�6\   6g   �� 5+      -   5        $ copyTeacherCodeButton��5+      $��Copy Teacher Code��64   $	��5 1q�6P   $��5q�6^   6i   �� 5,      -   5        $$ EditTeseterQuestionnaireButton��5,      $��Edit Patient Questions��6=   $	��5 2q�6^   $��5q�6l   6w   �� 5.      -   5        $ newTesterButton��5.      $��Create New Patient��6.   $	��5 3q�6K   $��5q�6Y   6d   �� 5/      -   5        $  preloadQuestionnaireButton��5/      $��Preload Questionnaire��69   $	��5 2q�6Y   $��5q�6g   6r   �� 50      -	   5        $ logoutButton��50      $��Logout��6+   $	��5 2q�6<   $��5q�6J   6U   �� 51      -
   5        $ 
exitButton��51      $
��Exit��6)   $	��5 2q�68   $��5q�6F   6Q   52      55      56      