   M S B A M L   `   `   `  ����[  WQuestion Maker Pro WPF Prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:Question_Maker_Pro_WPF_Prototype.Windows(Question_Maker_Pro_WPF_Prototype.Windows  5   	   B    <Question_Maker_Pro_WPF_Prototype.Windows.CreateAccountWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Hlocal6clr-namespace:Question_Maker_Pro_WPF_Prototype.Windows     5   	     � Title$  CreateAccountWindow��5   	   $	��450��6%   $	��800��62   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.3* �6   �� 5      $��* �6   �� 5      $
��0.2* �6   .+�5   
   �� $��Account Creation��6   $��Center=�6+   $��Center=�6H   $
��Bold"�6c   $
��20pt(�6u   �� 5   
   
�������$��1��6(   $��1q�65    �� CornerRadius$ 0��6I   $��25q�6Z   ._�5      �� $��1��6   $
��Auto=�6(   $��10q�6K   $��5q�6X   $
��10ptq�6c   
����  �.��5      �� $��10q�6   .+�5      �� $��Email��6   �� 5      -   5         ��Name$ emailTextBox��5      $	��250��64   $
��Left=�6@   $��7 0 0 10q�6[   �� 5      $��Password��6   G� 5       -   5         G�Name$ passwordEntry��5    "   $	��250��69   $
��Left=�6E   $��7 0 0 10q�6`   �� 5"      $��Confirm Password��6   G� 5#      -   5        $ confirmPasswordEntry��5#   "   $	��250��6@   $
��Left=�6L   $��7 0 0 10q�6g   �� 5%      $��
First Name��6   �� 5&      -   5        $ firstNameTextbox��5&      $	��250��68   $
��Left=�6D   $��7 0 0 10q�6_   �� 5(      $��	Last Name��6   �� 5)      -   5        $ lastNameTextbox��5)      $	��250��67   $
��Left=�6C   $��7 0 0 10q�6^   �� 5+      $��Organization��6   �� 5,      -   5        $ organizationTextBox��5,      $	��250��6;   $
��Left=�6G   $��7 0 0 10q�6b   A� 5.       A� Columns$ 2��6"   $��Right=�6.   $��	5 20 25 5q�6J   .+�5/      �� -   5         ��Name$ submitButton��5/   !   $��Submit��67   $
��10 4q�6H   $��0 0 5 0q�6W   6h   �� 50      -   5        $ cancelButton��50   !   $��Cancel��67   $
��10 4q�6H   $��5 0 0 0q�6W   6h   51      53      54      55      56      57      