   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfAdd_KHB   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��AddKHB��5   =     � Title$  Nhập số dư��6	   $��352.418��6   $��512.104��6-    � WindowStartupLocation$ CenterScreen=�6K    � Icon$ Icons/VBSP.ico��6p    � 
ResizeMode$ CanMinimize=�6�    � WindowStyle$ ThreeDBorderWindow=�6�   .��5      � $��0,0,0,11q�6   .+�5   
   �� -   5        $��	lblManual��5   Y   $��Lấy dữ liệu��6   $
��Left=�6&   $��363,25,0,248.2q�6A    J� RenderTransformOrigin$ 0.5,0.5&�6�   $	��131��6�   z�5      V� 5      .~�5	      �� �� 5
      �� 5       �� Angle$ 0.591Q�6&   T� 5      5      ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5      	 P� Property	 �@J�IsMouseOver
 P� Value
 . . �5      ��  �� Property � �� �� Value
 �  ��6C   �� 5       � ��$ Italic$�6;   �� 5       � ��$
 Bold"�6C   5      .�5      ��  � ��
 � � �6;   �� 5       � ��$ Times New Roman)�6;   �� 5       �@�ToolTip$/ )Click vào đây để nhập số liệu��60   5      5      �� 5   
   -   5        $��OK��5      $
��Save��6   $
��Left=�6    $��24��6;   $��168,272,0,0q�6G   $	��Top=�6\   $��84��6t   ��5       �� 5!      " ��6   .�5"      ��  � �� 5#      �� 5$       �� 
TargetType" ��6.   .R�5%   "   �� $��border��6)   $��1q�67    �� Padding$	 4,2q�6K   
������� �� CornerRadius$ 3��6p   #��z��6�   ._�5&   &   �� $��content��6o   $��Center=�67   $��Center=�6T   6o   5'   #   5(      5*       5+      P� 5,      	 �@J�IsMouseOver
 . . �5-      ��  � ��
 �/���6;   5.      50      53      �� 54   
   -   5        $
��Exit��54   �   $��Close��6   $
��Left=�6!   $��24��6<   $��280,272,0,0q�6H   $	��Top=�6]   $��84��6u   ��55      �� 56      " ��6   .�57      ��  � �� 58      �� 59      " ��6.   .R�5:   "   �� $��border��6)   $��1q�67   $	 4,2q�6K   
�������$ 3��6p   #��z��6�   ._�5;   &   �� $��content��6o   $��Center=�67   $��Center=�6T   6o   5<   #   5=      5?       5@      P� 5A      	 �@J�IsMouseOver
 . . �5B      ��  � ��
 �/���6;   5C      5E      5H      (  "System.Windows.Controls.DatePicker  5K   
   -   5        $��dtpNgay��5K      $
��Left=�6$   $��23��6?   $��	60,28,0,0q�6K   $	��Top=�6^   $	��104��6v   �� 5L   
   -   5         ��Name$ lblNgay��5L   Y   $��Ngày��6   $��26��6   $
��Left=�6+   $��	10,27,0,0q�6F   $	��Top=�6j   $��42��6�   $ -0.071,0.423&�6�   &   System.Windows.Controls.DataGrid  5M   
   -   5        $��	dgvTarGet��5M   �      AutoGenerateColumns .  $	��193��6/      CanUserDeleteRows .     CanUserAddRows .  $
��Left=�6m   $��3,71,0,0q�6�   $	��Top=�6�   $	��493��6�      Columns 5N      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Header$ Cấp��6G      Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 6T      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$ DT_CAPDT�6_     5P      $ Mã PGD��6G   $	 100 6W    .  6%   �� $ DT_MAPGD�6c     5Q      $ Chương trình��6G   $	 250 6]    .  6%   �� $ TEN_CHTR�6i     5R      $ Số dư cuối��6E   $ * 6Y    6%   �� $ DUCUOI�6Y   5T      �� 5U   
   -   5         ��Name$ cboPos��5U   N   $��22��6   $
��Left=�6   $��
212,27,0,0q�6:   $	��Top=�6^   $	��139��6v   �� 5V   
   -	   5        $ lblPos��5V   Y   $	��POS��6   $��27��6   $
��Left=�6*   $��
169,26,0,0q�6E   $	��Top=�6i   $��43��6�   ���	#FF1393136�   5X      5Y      