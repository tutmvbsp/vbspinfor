   M S B A M L   `   `   `  ����  Presentation     Presentation.WpfGiaoNhanTV   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$"  Chia tách, sáp nhập tổ��5   	   $��577.497��6(   $��1038.63��69    � WindowStartupLocation$ CenterScreen=�6I    � Icon$ /Icons/Connected.ico��6o   .��5      � .+�5   
   (  "System.Windows.Controls.DatePicker  -   5        $��dtpNgay��5   O   $��26��6   $
��Left=�6!   $��	73,19,0,0q�6<   $	��Top=�6^   $	��110��6v   �� 5   
   -   5        $��lblNgay��5   [   $��Ngày��6   $
��auto��6   $
��Left=�6-   $��	23,19,0,0q�6H   $	��Top=�6j   $
��auto��6�   �� 5   
   -   5        $��lblPos��5   [   $	��POS��6   $
��auto��6   $
��Left=�6,   $��
191,18,0,0q�6G   $	��Top=�6i   $
��auto��6�   �� 5	   
   -   5        $��CboPos��5	   N   $��22��6   $
��Left=�6   $��
232,19,0,0q�6:   $	��Top=�6\   $	��110��6t   6�   �� 5
   
   -   5        $��lblXa��5
   Z   $	��Xã��6   $
��auto��6   $
��Left=�6+   $��
348,18,0,0q�6F   $	��Top=�6g   $
��auto��6   �� 5   
   -   5        $��CboXa��5   N   $��22��6   $
��Left=�6   $��
393,19,0,0q�6:   $	��Top=�6[   $	��110��6s   6   �� 5   
   -   5        $��btnClose��5   ]   $��Close��6   $��22��6!   $
��Left=�6-   $��931,508,0,0q�6H   $	��Top=�6m   $��90��6�   6�   �� 5   
   -	   5        $��btnOk��5   Z   $��OK��6   $��22��6   $
��Left=�6*   $��811,508,0,0q�6E   $	��Top=�6g   $��90��6   6�   �� 5   
   -
   5        $��	lblToGiao��5   _   $��	Tổ Giao��6   $
��auto��6"   $
��Left=�60   $��
524,18,0,0q�6K   $	��Top=�6p   $
��auto��6�   �� 5   
   -   5        $��	CboToGiao��5   N   $��22��6   $
��Left=�6   $��
585,19,0,0q�6:   $	��Top=�6_   $	��110��6w   �� 5   
   -   5        $��	lblToNhan��5   _   $��Tổ Nhận��6   $
��auto��6"   $
��Left=�60   $��
712,18,0,0q�6K   $	��Top=�6p   $
��auto��6�   �� 5   
   -   5        $��	CboToNhan��5   N   $��22��6   $
��Left=�6   $��
778,19,0,0q�6:   $	��Top=�6_   $	��110��6w   �� 5   
   -   5        $��
lblGetData��5   T    �� Left$	 735��6   
 �� Top$ 15��6"   $��Lấy dữ liệu��62   $��27��6H   $
��auto��6f   $��914,19,32,501q�6�   ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      �� 	 �� Property	 � ��
 �� Value

 �  ��6C   �� 5      	 � ��$

 Bold"�6C   5      .�5      �� 	 � ��

 � � �6;   �� 5      	 � ��$
 Comic Sans MS)�6;   �� 5      	 � ��$

 Bold"�6;   5      5       &   System.Windows.Controls.DataGrid  5!   
   -   5        $��	dgvSource��5!   j      AutoGenerateColumns .  $	��422��6/   $
��Left=�6<   $��	23,71,0,0q�6W   $	��Top=�6{   $	��998��6�      Columns 5"      4  .System.Windows.Controls.DataGridCheckBoxColumn  5#      ,  &System.Windows.Controls.DataGridColumn   CanUserReorder .     CanUserResize .     CanUserSort .     Header$ Chọn��6�      Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   ��  �� Path$
 CHON�6�   0  *System.Windows.Controls.DataGridTextColumn  5$      $ Tên khách hàng��6G   $	 200 6_    6%   �� $ KH_TENKH�6_     5%      $ Mã khách hàng��6F   $	 200 6]    6%   �� $ KH_MAKH�6]     5&      $ Chương trình��6G   $	 200 6]    6%   �� $ SC_TENVT�6]     5'      $ Dư nợ��6C   $	 200 6R    6%   �� $
 DUNO�6R     5(      $ Dư tiết kiệm��6C   $	 200 6Y    6%   �� $
 DUTK�6Y   5*      5+      5,      