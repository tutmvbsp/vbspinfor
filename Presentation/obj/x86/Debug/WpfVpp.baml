   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfVpp   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$*  $Giấy xin nhận văn phòng phẩm��5   	   $��577.497��6.   $��846.63��6?    � WindowStartupLocation$ CenterScreen=�6N    � Icon$ /Icons/Connected.ico��6t   .��5      � .+�5   
   �� -   5        $��lblNgay��5   \   $��Tháng��6   $
��auto��6    $
��Left=�6.   $��	23,18,0,0q�6I   $	��Top=�6k   $
��auto��6�   �� 5   
   -   5        $��lblPos��5   [   $	��POS��6   $
��auto��6   $
��Left=�6,   $��
360,18,0,0q�6G   $	��Top=�6i   $
��auto��6�   �� 5   
   -   5        $��lblNam��5   [   $
��Năm��6   $
��auto��6   $
��Left=�6,   $��
197,18,0,0q�6G   $	��Top=�6i   $
��auto��6�   �� 5	   
   -   5        $��CboPos��5	   N   $��22��6   $
��Left=�6   $��
402,19,0,0q�6:   $	��Top=�6\   $��88��6t   �� 5
   
   -   5        $��lblXa��5
   `   $��Phòng/Tổ��6   $
��auto��6#   $
��Left=�61   $��
494,18,0,0q�6L   $	��Top=�6m   $
��auto��6�   �� 5   
   -   5        $��CboPB��5   N   $��22��6   $
��Left=�6   $��
563,19,0,0q�6:   $	��Top=�6[   $	��110��6s   �� 5   
   -   5        $��btnClose��5   ]   $��Close��6   $��22��6!   $
��Left=�6-   $��729,508,0,0q�6H   $	��Top=�6m   $��90��6�   6�   �� 5   
   -	   5        $��btnSua��5   [   $��Sửa��6   $��22��6   $
��Left=�6+   $��321,508,0,0q�6F   $	��Top=�6i   $��90��6�   6�   �� 5   
   -
   5        $��btnXoa��5   [   $
��Xóa��6   $��22��6   $
��Left=�6+   $��427,508,0,0q�6F   $	��Top=�6i   $��90��6�   6�   �� 5   
   -   5        $��btnLuu��5   [   $
��Lưu��6   $��22��6   $
��Left=�6+   $��529,508,0,0q�6F   $	��Top=�6i   $��90��6�   6�   �� 5   
   -   5        $��btnIn��5   Z   $��In��6   $��22��6   $
��Left=�6*   $��627,508,0,0q�6E   $	��Top=�6g   $��90��6   6�   �� 5   
   -   5        $��
lblGetData��5   H    �� Left$	 735��6   
 �� Top$ 15��6"   $��Lấy dữ liệu��62   $
��auto��6Z   $��681,20,62,482q�6�   ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      �� 	 �� Property	 � ��
 �� Value

 �  ��6C   �� 5      	 � ��$

 Bold"�6C   5      .�5      �� 	 � ��

 � � �6;   �� 5      	 � ��$
 Comic Sans MS)�6;   �� 5      	 � ��$

 Bold"�6;   5      5      &   System.Windows.Controls.DataGrid  5    
   -   5        $��	dgvSource��5    �      AutoGenerateColumns .  $	��422��6/      CanUserDeleteRows .     CanUserAddRows .  $
��Left=�6m   $��	23,71,0,0q�6�   $	��Top=�6�   $	��806��6�      Columns 5!      4  .System.Windows.Controls.DataGridCheckBoxColumn  5"      ,  &System.Windows.Controls.DataGridColumn   CanUserReorder .     CanUserResize .     CanUserSort .     Header$ Chọn��6�      Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ��  �� Path$
 CHON�6�   0  *System.Windows.Controls.DataGridTextColumn  5#      $ MA��6A   $ 50 6M    6%   �� $ MA�6M     5$      $ 
Tên hàng��6B   $	 500 6T    6%   �� $	 TEN�6T     5%      $ 
Đơn Vị��6D   $	 100 6T    6%   �� $ DONVI�6T     5&      $ Số lượng��6F   $	 100 6X    6%   �� $ SOLUONG�6X   5(      �� 5)   
   -   5         ��Name$ comboBoxMonth��5)   M   $��22��6   $
��Left=�6   $��	77,19,0,0q�6:   $	��Top=�6d   $	��110��6|   �� 5*   
   -   5        $ comboBoxYear��5*   N   $��22��6   $
��Left=�6   $��
245,19,0,0q�6:   $	��Top=�6d   $	��110��6|   5+      5,      