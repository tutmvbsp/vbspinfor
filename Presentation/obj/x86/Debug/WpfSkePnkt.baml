   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfSkePnkt   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Sao kê tổ theo PNKT��5   	   $	��516��6%   $��1004.239��62    � WindowStartupLocation$  CenterScreen=�6C    � Icon$ Icons/My Video.ico��6j   .��5      � .+�5   
   �� $��Export��6   $
��Left=�6#   $��46��6>   $��
855,10,0,0q�6J   $	��Top=�6^   $	��131��6v   .��5      � $��0,0,-2,0q�6    � ColumnDefinitions 5      �� 5	      $��9* �6'   �� 5
      $��2* �6'   .+�5      	� -   5        $
��Opt1��5   +   $��In��6   $
��Left=�67   $��21��6R   $��18,3,0,0q�6^   $	��Top=�6p   $��32��6�   	� 5      -   5        $
��Opt2��5   .   $��Excel��6   $
��Left=�6:   $��21��6U   $��67,3,0,0q�6a   $	��Top=�6s   $��59��6�   $��2��6�   $��0��6�    d� 	IsChecked$
 TrueY�6�   5      5      �� 5   
   -   5        $��btnClose��5   ]   $��Close��6   $��27��6!   $
��Left=�6-   $��588,442,0,0q�6H   $	��Top=�6m   $��75��6�   6�   �� 5   
   -   5        $��btnOk��5   Z   $��OK��6   $��27��6   $
��Left=�6*   $��423,442,0,0q�6E   $	��Top=�6g   $��75��6   6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dtpNgay��5   O   $��24��6   $
��Left=�6!   $��	58,19,0,0q�6<   $	��Top=�6^   $	��111��6v   �� 5   
   -   5        $��lblNgay��5   X   $��Ngày��6   $��26��6   $
��Left=�6+   $��6,19,0,0q�6F   $	��Top=�6g   $��42��6   �� 5   
   -   5        $��CboPos��5   N   $��23��6   $
��Left=�6   $��
233,21,0,0q�6:   $	��Top=�6\   $	��111��6t   6�   �� 5   
   -	   5        $��lblPos��5   Y   $	��POS��6   $��26��6   $
��Left=�6*   $��
181,23,0,0q�6E   $	��Top=�6g   $��39��6   �� 5   
   -
   5        $��CboTo��5   N   $��23��6   $
��Left=�6   $��
630,22,0,0q�6:   $	��Top=�6[   $	��111��6s   �� 5   
   -   5        $��CboXa��5   N   $��23��6   $
��Left=�6   $��
425,21,0,0q�6:   $	��Top=�6[   $	��111��6s   6   �� 5   
   -   5        $��lblTo��5   X   $
��Tổ��6   $��26��6   $
��Left=�6)   $��
578,19,0,0q�6D   $	��Top=�6e   $��39��6}   �� 5   
   -   5        $��lblXa��5   X   $	��Xã��6   $��26��6   $
��Left=�6)   $��
375,17,0,0q�6D   $	��Top=�6e   $��39��6}   �� 5   
   -   5        $��LoadData��5   W   $��Lấy số liệu��6   $
��Left=�6&   $��768,22,0,431q�6A    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��98��6�   z�5      V� 5      .~�5      �� �� 5       �� 5!       �� Angle$ 0.591Q�6&   T� 5"      5#      ��5%      �� 5&       �� 
TargetType" ��6    �� Triggers 5'      P� 5(      	 P� Property	 �@J�IsMouseOver
 P� Value
 . . �5)      ��  �� Property � �� �� Value
 �  ��6C   �� 5*       � ��$ Italic$�6;   �� 5+       � ��$
 Bold"�6C   5,      .�5.      ��  � ��
 � � �63   �� 5/       � ��$
 Bold"�63   �� 50       � ��$ Times New Roman)�63   �� 51       �@�ToolTip$/ )Click vào đây để nhập số liệu��60   52      54      &   System.Windows.Controls.DataGrid  56   
   -   5        $��	dgvSource��56   j      AutoGenerateColumns .  $	��341��6/   $
��Left=�6<   $��	10,89,0,0q�6W   $	��Top=�6{   $	��976��6�    � 	Resources 57      =  7System.Windows.Controls.Primitives.DataGridColumnHeader��%|   '        �� 58      "  6   .�59      ��  � ��
 �����63   �� 5:       � ��$ Center=�6C   5;      5<         Columns 5=      4  .System.Windows.Controls.DataGridCheckBoxColumn  5>      ,  &System.Windows.Controls.DataGridColumn   Header$ Chọn��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *Chon+0  *System.Windows.Controls.DataGridTextColumn  5?      $ Tên KH��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6S      FontSize$ 13Q�6`    65   �� *
KH_TENKH+6`     5@      $ Số KU��6%   $
 auto 6Q   $ 13Q�6^    64   �� *	KU_SOKU+6^     5A      $ Chương Trình��6%   $
 auto 6V   $ 13Q�6c    6;   �� *TENVT+6c     5B      $ Dư Nợ��6%   $
 auto 6N   $ 13Q�6[    64   �� *DUNO+6[     5C      $ Mục đích SD vốn��6%   $ * 6X   $ 13Q�6b    6>   �� *PNKT+6b   5E      �� 5F   
   -   5        $��ChkAll��5F   `   $��
Tất cả��6   $
��auto��6$   $
��Left=�62   $��	21,68,0,0q�6M   $	��Top=�6n   $
��auto��6�   ���	#FFD81313$
��Bold"�6�   5G      5H      