   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages&Question_Maker_Pro_WPF_Prototype.Pages  5      >    8Question_Maker_Pro_WPF_Prototype.Pages.NewTesterInfoPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:Question_Maker_Pro_WPF_Prototype.Pages     5        N� Title$  NewTesterInfoPage��5	      .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��0.3* �6   �� 5      .+�5   
   �� $��1��6   $��Input Patient Info��6    $��Center=�6=   $��Center=�6Z   $
��36pt(�6u   $
��Bold"�6�   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��1* �6#   �� 5      $��1* �6#   �� 5      $��1* �6#   .+�5      �� -   5         ��Name$ firstNameTextBox��5      $��Center=�60   $��1��6M   $��0��6Z   $��
25,61,25,0q�5       �� TextWrapping$
 Wrap=�6*   $�� ��6>   $	��Top=�6F   $	��120��6^   $
��12pt(�6j   �� 5      $��
First Name��6   $��Center=�6)   $��-25,35,25,0q�6F   $	��Top=�6[   $
��12pt(�6s   �� 5      -   5        $ lastNameTextBox��5      $��1��6/   $��Center=�6?   $��0,61,0,0q�6\   $
 Wrap=�6n   $�� ��6�   $	��Top=�6�   $	��120��6�   $
��12pt(�6�   �� 5      $��	Last Name��6   $��1��6(   $��Center=�68   $��
-40,35,0,0q�6U   $	��Top=�6i   $
��12pt(�6�   (  "System.Windows.Controls.DatePicker  5       -   5          Name$ 	dobPicker��5       $��1��6,   $��Center=�6<   $��	0,256,0,0q�6Y   $	��Top=�6l   $
��12pt(�6�   �� 5!      $��1��6   $��Date of Birth��6$   $��Center=�6<   $��-40,230,0,0q�6Y   $	��Top=�6n   $
��12pt(�6�   �� 5#      $��2��6   $��Gender��6$   $
��Left=�65   $��	67,35,0,0q�6P   $	��Top=�6c   $
��12pt(�6{   	� 5$      -   5         	�Name$ maleRadioButton��5$      $��2��63   $
��Male��6C   $
��Left=�6R   $��
71,65,25,0q�6m   $	��Top=�6�   $
��12pt(�6�   	� 5%      -   5        $ femaleRadioButton��5%      $��2��65   $��Female��6E   $
��Left=�6V   $��
71,90,25,0q�6q   $	��Top=�6�   $
��12pt(�6�   	� 5&      -   5        $ otherRadioButton��5&      $��2��64   $��Other��6D   $
��Left=�6T   $��71,115,25,0q�6o   $	��Top=�6�   $
��12pt(�6�   A� 5(      $��1��6   $��2��6'    A� Rows$ 1��67    A� Columns$ 2��6@   $��Center=�6L   $��
5,285,5,25q�6i   .+�5)      �� -   5        	 ��Name$	 submitButton��5)      $��Submit��6/   $��5q�6@   $��25��6K   $��75��6W   $��5q�6b   6n   �� 5*      -   5        $	 
backButton��5*      $
��Back��6-   $��5q�6<   $��5q�6G   $��25��6S   $��75��6_   6j   5+      5-      50      52      