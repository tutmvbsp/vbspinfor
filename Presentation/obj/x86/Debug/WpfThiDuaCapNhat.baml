   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    Presentation#   Presentation.WpfThiDuaCapNhat   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o	 kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b
 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation   	 
     5   	   -   5          � Title$  Cập nhật chỉ tiêu��5   	   $��531.418��6#   $��1170.904��64    � WindowStartupLocation$ CenterScreen=�6E    � Icon$ Icons/VBSP.ico��6j    � 
ResizeMode$ CanMinimize=�6�    � WindowStyle$ ThreeDBorderWindow=�6�   .��5      � $��0,0,0,11q�6   .+�5
   
   �� -   5         ��Name$ OK��5
      $
��Save��6   $
��Left=�6    $��24��6;   $��433,449,0,0q�6G   $	��Top=�6\   $��84��6t   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      	 �� 
TargetType"	 ��6.   .R�5   "   �� 
 ��Name$
 border��6)   $��1q�69    �� Padding$	 4,2q�6M   
������� �� CornerRadius$ 3��6r   #��z��6�   ._�5   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
 �/���6;   5      5      5      �� 5    
   -   5        $ btnClose��5    �   $��Close��6   $
��Left=�6!   $��24��6<   $��574,449,0,0q�6H   $	��Top=�6]   $��84��6u   ��5!      �� 5"      " ��6   .�5#      ��  � �� 5$      �� 5%      "	 ��6.   .R�5&   "   �� $
 border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�5'   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5(   #   5)      5+       5,      P� 5-       �@J�IsMouseOver . . �5.      ��  � ��
 �/���6;   5/      51      54      &   System.Windows.Controls.DataGrid  57   
   -   5          Name$ 	dgvTarGet��57   �      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $	��346��6d   $
��Left=�6q   $��	10,70,0,0q�6�   $	��Top=�6�   $
��1145��6�      RowStyle 59      �� 5:      )  #System.Windows.Controls.DataGridRow"  6   .�5;      ��  � ��
 X� NaN# Z 6/   5<         Columns 5>      0  *System.Windows.Controls.DataGridTextColumn  5?      ,  &System.Windows.Controls.DataGridColumn   Header$	 STT��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6I      
IsReadOnly .     FontSize$ 14Q�6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *TT+   HeaderStyle 5@      �� 5A      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5B      ��  � ��$ Center=�6K   5C         ElementStyle 5E      �� 5F      " ��6    .�5G      ��  �@��TextAlignment$ Right=�6>   �� 5H       � ��$ Center=�6B   5I      5K        5L      $ Chỉ Tiêu��6%   $ * 6S    .   67   �� *	CHITIEU+ 5M      �� 5N      "  6    .�5O      ��  � ��$ Center=�6K   5P       5R      �� 5S      " ��6    .�5T      ��  �@��TextAlignment$
 Left=�6>   �� 5U       � ��$
 Wrap=�6=   �� 5V       � ��$ Center=�6B   5X      5Z        5[      $ Điểm Tối Đa��6%   $ 80 6i    .     
FontWeight$
 Bold"�6�    6:   �� *DIEM+  �� StringFormat$  {0:n1}�� 5\      �� 5]      "  6    .�5^      ��  � ��$ Center=�6K   5_       5a      �� 5b      " ��6    .�5c      ��  �@��TextAlignment$ Center=�6>   �� 5d       � ��$ Center=�6B   5e      5g        5h      $ 	In đậm��6%   $ 50 6O    65   �� *INDAM+ 5i      �� 5j      "  6    .�5k      ��  � ��$ Center=�6K   5l       5n      �� 5o      " ��6    .�5p      ��  �@��TextAlignment$ Center=�6>   �� 5q       � ��$ Center=�6B   5r      5t        5u      $ In nghiêng��6%   $ 70 6U    69   �� *	NGHIENG+ 5v      �� 5w      "  6    .�5x      ��  � ��$ Center=�6K   5y       5{      �� 5|      " ��6    .�5}      ��  �@��TextAlignment$ Center=�6>   �� 5~       � ��$ Center=�6B   5      5�        5�      $ Cộng mức��6%   $ 65 6P    .   67   �� *CONG+ 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$
 Wrap=�6=   �� 5�       � ��$ Center=�6B   5�      5�      5�      �� 5�   
   -   5        ! ��Name$! lblQuy��5�   [   $
��Qúy��6   $
��auto��6   $
��Left=�6,   $��
175,15,0,0q�6G   $	��Top=�6k   $
��auto��6�   �� 5�   
   -   5        $! lblPos��5�   [   $	��POS��6   $
��auto��6   $
��Left=�6,   $��
402,15,0,0q�6G   $	��Top=�6k   $
��auto��6�   �� 5�   
   -   5        $! lblNam��5�   [   $
��Năm��6   $
��auto��6   $
��Left=�6,   $��
287,15,0,0q�6G   $	��Top=�6k   $
��auto��6�   *  $Telerik.Windows.Controls.RadComboBox  5�   
   -   5        "  Name$" 	RadCboPos��5�      $
��Left=�61   $��
440,16,0,0q�6L   $	��Top=�6`   $	��123��6x   �� 5�   
   -	   5        $! lblXa��5�   c   $��Đợt thi đua��6   $
��auto��6&   $
��Left=�64   $��
575,15,0,0q�6O   $	��Top=�6r   $
��auto��6�   �� 5�   
   -
   5        $! lblChDe��5�   a   $��Chuyên đề��6   $
��auto��6$   $
��Left=�62   $��
770,15,0,0q�6M   $	��Top=�6r   $
��auto��6�     5�   
   -   5        $" 	RadCboDot��5�      $
��Left=�61   $��
650,16,0,0q�6L   $	��Top=�6`   $	��110��6x     5�   
   -   5        $" 
RadCboChDe��5�      $
��Left=�62   $��
847,16,0,0q�6M   $	��Top=�6a   $	��110��6y   �� 5�   
   -   5        $! 
lblGetData��5�   &   $��Lấy dữ liệu��6   $
��auto��6:   $
��auto��6G   $��971,15,70.8,438.6q�6x   ��5�      �� 5�      " ��6   .�5�      ��  � ��
 � � �63   �� 5�       � ��$ Comic Sans MS)�63   �� 5�       � ��$
 Bold"�63    5�      P� 5�       �@J�IsMouseOver . . �5�      ��  � ��
 �  ��6;   �� 5�       � ��$
 Bold"�6;   5�      5�      5�      (	  "System.Windows.Controls.DatePicker	  5�   
   -   5        # 	 Name$# dtpNgay��5�   O   $��26��6   $
��Left=�6!   $��	61,15,0,0q�6<   $	��Top=�6`   $	��110��6x   �� 5�   
   -   5        $! lblNgay_Copy��5�   Y   $��Ngày��6   $��30��6   $
��Left=�6+   $��	18,15,0,0q�6F   $	��Top=�6o   $��46��6�   $ J� RenderTransformOrigin$$ -2.191,-0.167&�6�     5�   
   -   5        $" comboBoxYear��5�      $
��Left=�64   $��
325,16,0,0q�6O   $	��Top=�6c   $��64��6{     5�   
   -   5        $" CboQuy��5�      $
��Left=�6.   $��22��6I   $��
215,16,0,0q�6U   $	��Top=�6i   $��57��6�   % �� SelectedIndex$% 0��6�   .@�5�      .
  (Telerik.Windows.Controls.RadComboBoxItem
  $��1��6&   
  5�      $��2��6&   
  5�      $��3��6&   
  5�      $��4��6&   5�      �� 5�   
   -   5        & ��Name$& chkth��5�      $��In biểu tổng hợp��6"   $
��Left=�6=   $��
847,47,0,0q�6X   $	��Top=�6l   $	��110��6�   5�      5�      