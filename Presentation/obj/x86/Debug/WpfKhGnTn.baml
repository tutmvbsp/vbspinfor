   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfKhGnTn   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$6  0Nhập kế hoạch giải ngân thu nợ tuần��5   	   $	��564��65   $	��991��6B    � WindowStartupLocation$  CenterScreen=�6O   .��5      � .+�5   
   (  "System.Windows.Controls.DatePicker  -   5        $��dtpNgay��5   O   $��22��6   $
��Left=�6!   $��	79,10,0,0q�6<   $	��Top=�6^   $	��132��6v   �� 5   
   -   5        $��lblNgay��5   Y   $��Ngày��6   $��23��6   $
��Left=�6+   $��	17,10,0,0q�6F   $	��Top=�6h   $��56��6�   �� 5   
   -   5        $��lblPos��5   Y   $	��POS��6   $��23��6   $
��Left=�6*   $��
234,10,0,0q�6E   $	��Top=�6g   $
��auto��6   �� 5	   
   -   5        $��lblXa��5	   X   $	��Xã��6   $��23��6   $
��Left=�6)   $��
442,10,0,0q�6D   $	��Top=�6e   $
��auto��6}   �� 5
   
   -   5        $��CboPos��5
   M   $��26��6   $
��Left=�6   $��	282,7,0,0q�6:   $	��Top=�6[   $	��149��6s   6   �� 5   
   -   5        $��CboXa��5      $��494,7,326,492q�6    $��26��67   $	��149��6C   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��5q�6#    d� 	IsChecked 6.   �� *Chon+�� 5      $��5q�6$   ��6/   �� *Maxa+�� 5      $��5q�6$   ��6/   �� *Tenxa+5      5      5      �� 5   
   -   5        $��btnclose��5   ]   $��Close��6   $��22��6!   $
��Left=�6-   $��529,473,0,0q�6H   $	��Top=�6m   $��91��6�   6�   �� 5   
   -	   5        $��btnOk��5   Z   $��OK��6   $��22��6   $
��Left=�6*   $��340,473,0,0q�6E   $	��Top=�6g   $��91��6   6�   �� 5   
   -
   5        $��btnChon��5   Y   $��Mới��6   $��20��6   $
��Left=�6+   $��	675,7,0,0q�6F   $	��Top=�6h   $��91��6�   6�   �� 5   
   -   5        $��btnHuy��5   Z   $��Hủy��6   $��22��6   $
��Left=�6+   $��
772,35,0,0q�6F   $	��Top=�6h   $��91��6�   6�   �� 5   
   -   5        $��btnSua��5   Z   $��Sửa��6   $��22��6   $
��Left=�6+   $��
675,35,0,0q�6F   $	��Top=�6h   $��91��6�   6�   �� 5   
   -   5        $��btnXoa��5   Y   $
��Xóa��6   $��22��6   $
��Left=�6+   $��	772,5,0,0q�6F   $	��Top=�6g   $��91��6   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��	dgvTarGet��5   �      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .    	RowHeight$ 30Q�6a   $	��258��6s   $
��Left=�6�   $��
28,158,0,0q�6�   $	��Top=�6�   $	��920��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Thứ��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 6O   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   ��  �� Path$	 Thu�   HeaderStyle 5      �� 5        �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5!      ��  �� Property � �� �� Value$ Center=�6K   5"         ElementStyle 5$      �� 5%      " ��6    .�5&      ��  �@��TextAlignment$ Center=�6>   �� 5'       � ��$ Center=�6B   5(      5*        5+      $	 Ngày��6%   $

 auto 6Q    63   �� $
 Ngay� 5,      �� 5-      "  6    .�5.      ��  � ��$ Center=�6K   5/       51      �� 52      " ��6    .�53      ��  �@��TextAlignment$ Center=�6>   �� 54       � ��$ Center=�6B   55      57        58      $		 Xã��6%   $

 auto 6P    61   �� $ Tenxa� 59      �� 5:      "  6    .�5;      ��  � ��$ Center=�6K   5<       5>      �� 5?      " ��6    .�5@      ��  �@��TextAlignment$ Center=�6>   �� 5A       � ��$ Center=�6B   5B      5D        5E      $	 Giải Ngân��6%   $	
 120 6Z    68   �� $ GiaiNgan� 5F      �� 5G      "  6    .�5H      ��  � ��$ Center=�6K   5I       5K      �� 5L      " ��6    .�5M      ��  �@��TextAlignment$ Center=�6>   �� 5N       � ��$ Center=�6B   5O      5Q        5R      $	 	Chi Khác��6%   $	
 120 6X    67   �� $ ChiKhac� 5S      �� 5T      "  6    .�5U      ��  � ��$ Center=�6K   5V       5X      �� 5Y      " ��6    .�5Z      ��  �@��TextAlignment$ Center=�6>   �� 5[       � ��$ Center=�6B   5\      5^        5`      $	 Thu nợ��6%   $	
 120 6T    65   �� $ ThuNo� 5a      �� 5b      "  6    .�5c      ��  � ��$ Center=�6K   5d       5f      �� 5g      " ��6    .�5h      ��  �@��TextAlignment$ Center=�6>   �� 5i       � ��$ Center=�6B   5j      5l        5m      $	 Thu lãi��6%   $	
 120 6V    66   �� $ ThuLai� 5n      �� 5o      "  6    .�5p      ��  � ��$ Center=�6K   5q       5s      �� 5t      " ��6    .�5u      ��  �@��TextAlignment$ Center=�6>   �� 5v       � ��$ Center=�6B   5w      5y        5z      $	 Thu tiết kiệm��6%   $	
 120 6[    6<   �� $ ThuTK� 5{      �� 5|      "  6    .�5}      ��  � ��$ Center=�6K   5~       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�        5�      $	 	Thu khác��6%   $
 * 6X    67   �� $ ThuKhac� 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   5�      5�      5�      �� 5�   
   -   5        $��lblTitle��5�   b   $��Nhập số liệu��6   $��33��6'   $
��Left=�63   $��
407,60,0,0q�6N   $	��Top=�6r   $	��111��6�   $
��Bold"�6�   
��� � �$��16(�6�   �� 5�   
   -   5        $��lblLuuy��5�   �   $p��jLưu ý : Đối với chọn sửa thì không cần chọn xã   - Chọn Mới,Xóa phải chọn xã  ��6   $��24��6i   $
��Left=�6u   $��
37,126,0,0q�6�   $	��Top=�6�   $
��auto��6�   $��Normal"�6�   
��� E��$��14(�6  �� 5�   
   -   5        $��label1��5�   l   $"��Đơn vị : Triệu đồng��6   $
��auto��6.   $
��Left=�6<   $��822,128,0,0q�6W   
���� ��$��Italic$�6�   $	��Top=�6�   $
��auto��6�   5�      5�      