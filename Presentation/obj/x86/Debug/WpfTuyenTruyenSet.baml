   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    Presentation$   Presentation.WpfTuyenTruyenSet   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o	 kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b
 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation   	 
     5   	   Idx2http://schemas.devexpress.com/winfx/2008/xaml/core   	 
     6K   Tdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol   	 
     6�   -   5          � Title$#  Thêm, sữa, xóa thông tin��5   	   $��412.513��6*   $��681.011��6;    � WindowStartupLocation$ CenterScreen=�6K    � Icon$ Icons/VBSP.ico��6p    � 
ResizeMode$ CanMinimize=�6�    � WindowStyle$ ThreeDBorderWindow=�6�   .��5      � $��0,-1,-0.2,11.6q�6   .+�5   
   �� -   5         ��Name$
 Them��5   d   $��Thêm��6   $��24��6    $��121,333,0,0q�6,   $	��Top=�6A   $��84��6Y   $
��Left=�6�   ��5	      �� 5
       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      	 �� 
TargetType"	 ��6.   .R�5   "   �� 
 ��Name$
 border��6)   $��1q�69    �� Padding$	 4,2q�6M   
������� �� CornerRadius$ 3��6r   #��z��6�   ._�5   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
 �/���6;   5      5      5      �� 5   
   -   5        $	 Sua��5   c   $��Sửa��6   $��24��6   $��229,333,0,0q�6+   $	��Top=�6@   $��84��6X   $
��Left=�6q   ��5      �� 5      " ��6   .�5       ��  � �� 5!      �� 5"      "	 ��6.   .R�5#   "   �� $
 border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�5$   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5%   #   5&      5(       5)      P� 5*       �@J�IsMouseOver . . �5+      ��  � ��
 �/���6;   5,      5.      50      �� 51   
   -   5        $ btnClose��51   d   $��Đóng��6   $��25��6    $��425,333,0,0q�6,   $	��Top=�6A   $��84��6Y   $
��Left=�6�   ��52      �� 53      " ��6   .�54      ��  � �� 55      �� 56      "	 ��6.   .R�57   "   �� $
 border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�58   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   59   #   5:      5<       5=      P� 5>       �@J�IsMouseOver . . �5?      ��  � ��
 �/���6;   5@      5B      5E      �� 5F   
   -   5        $ btnXoa��5F   c   $
��Xóa��6   $��25��6   $��328,333,0,0q�6+   $	��Top=�6@   $��84��6X   $
��Left=�6t   ��5G      �� 5H      " ��6   .�5I      ��  � �� 5J      �� 5K      "	 ��6.   .R�5L   "   �� $
 border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�5M   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5N   #   5O      5Q       5R      P� 5S       �@J�IsMouseOver . . �5T      ��  � ��
 �/���6;   5U      5W      5Z      &   System.Windows.Controls.DataGrid  5[   
   -   5          Name$ 	dgvTarGet��5[   �      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  $	��228��6d   $
��Left=�6q   $��	10,53,0,0q�6�   $	��Top=�6�   $	��655��6�      RowStyle 5]      �� 5^      )  #System.Windows.Controls.DataGridRow"  6   .�5_      ��  � ��
 X� NaN# Z 6/   5`         Columns 5b      0  *System.Windows.Controls.DataGridTextColumn  5c      ,  &System.Windows.Controls.DataGridColumn   Header$	 Mã��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6H      
IsReadOnly .    FontSize$ 14Q�6f   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *MA+   HeaderStyle 5d      �� 5e      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5f      ��  � ��$ Center=�6K   5g         ElementStyle 5i      �� 5j      " ��6    .�5k      ��  �@��TextAlignment$ Center=�6>   �� 5l       � ��$ Center=�6B   5m      5o        5p      $
 Tên��6%   $ * 6J    .  $ 14Q�6h    62   �� *TEN+ 5q      �� 5r      "  6    .�5s      ��  � ��$ Center=�6K   5t       5v      �� 5w      " ��6    .�5x      ��  �@��TextAlignment$ Center=�6>   �� 5y       � ��$ Center=�6B   5z      5|        5}      $ Mã thể loại��6%   $	 150 6Y    .  $ 14Q�6y    6:   �� *
MA_THELOAI+ 5~      �� 5      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�      5�      �� 5�   
   -   5         ��Name$ txtMa��5�      $
��Left=�6!   $
��auto��6<   $��
44,296,0,0q�6J     �� TextWrapping$
  Wrap=�6^   $�� ��6r   $	��Top=�6z   $��35��6�   �� 5�   
   -   5        $ txtTen��5�      $
��Left=�6"   $
��auto��6=   $��113,296,0,0q�6K   $
  Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��362��6�   �� 5�   
   -	   5        ! ��Name$! lblMa��5�   Z   $	��Mã��6   $
��auto��6   $
��Left=�6+   $��
16,296,0,0q�6F   $	��Top=�6i   $
��auto��6�   �� 5�   
   -
   5        $! lblTen��5�   [   $
��Tên��6   $
��auto��6   $
��Left=�6,   $��
88,296,0,0q�6G   $	��Top=�6k   $
��auto��6�   �� 5�   
   -   5        " ��Name$" groupBox��5�      $��GroupBox��6%   $
��Left=�67   $��48��6R   $��75,0,0,0q�6^   $	��Top=�6p   $	��483��6�   .��5�      � .+�5�      	� -   5        # 	�Name$# radio1��5�      $��Cấp��6.   $
��Left=�6<   $
��auto��6W   $��
10,-5,0,-6q�6e   $	��Top=�6y   $
��auto��6�   $ d� 	IsChecked$
$ TrueY�6�   	� 5�      -   5        $# radio2��5�      $��Thể loại��6.   $
��Left=�6A   $
��auto��6\   $��
90,-5,0,-6q�6j   $	��Top=�6~   $
��auto��6�   	� 5�      -   5        $# radio3��5�      $��Nguồn tin��6.   $
��Left=�6B   $
��auto��6]   $��190,-5,0,-6q�6k   $	��Top=�6�   $
��auto��6�   	� 5�      -   5        $# radio4��5�      $��Thời lượng��6.   $
��Left=�6C   $
��auto��6^   $��290,-5,0,-6q�6l   $	��Top=�6�   $
��auto��6�   	� 5�      -   5        $# radio5��5�      $��
Loại tin��6.   $
��Left=�6A   $
��auto��6\   $��390,-5,0,-6q�6j   $	��Top=�6   $
��auto��6�   5�      5�      �� 5�   
   -   5        $! GetForm��5�   V   $��	Load data��6   $
��Left=�6$   $��578,8,0,331.4q�6?   % J� RenderTransformOrigin$% 1.342,-2.636&�6�   $��69��6�   z�5�      V� 5�      .~�5�      �� �� 5�      �� 5�      & �� Angle$& 0.591Q�6&   T� 5�      ' T� X$' 1.127Q�6)   ( T� Y$( 0.854Q�63   5�      ��5�      �� 5�      " ��6   .�5�      ��  � ��
 � � �63   �� 5�       � ��$
 Bold"�63   �� 5�       � ��$ Times New Roman)�63   �� 5�       �@�ToolTip$/ )Click vào đây để nhập số liệu��60    5�      P� 5�       �@J�IsMouseOver . . �5�      ��  � ��
 � � �6;   �� 5�       � ��$ Italic$�6;   �� 5�       � ��$
 Bold"�6;   5�      5�      5�      �� 5�   
   -   5        $ txtMaTheLoai��5�      $
��Left=�6(   $
��auto��6C   $��550,296,0,0q�6Q   $
  Wrap=�6f   $�� ��6z   $	��Top=�6�   $��35��6�   �� 5�   
   -   5        $! lblMaTheLoai��5�   d   $��Mã thể loại��6   $
��auto��6&   $
��Left=�64   $��485,296,0,0q�6O   $	��Top=�6z   $
��auto��6�   5�      5�      