   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PresentationPresentation  5   	       Presentation.WpfKTNB_Set   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   	   ;presentationclr-namespace:Presentation    	 
    5   	   -   5          � Title$#  Cài đặt mẫu biểu KTNB��5   	   $��577.497��6'   $��713.83��68    � WindowStartupLocation$ CenterScreen=�6G    � Icon$ /Icons/Connected.ico��6m   .��5	      � .+�5
   
   �� -   5         ��Name$ lblPos��5
   [   $��Mẫu��6   $
��auto��6   $
��Left=�6,   $��
176,11,0,0q�6G   $	��Top=�6k   $
��auto��6�   * 
 $Telerik.Windows.Controls.RadComboBox  5   
   -   5          Name$ CboMau��5   Y   $��22��6   $
��Left=�6*   $��
226,12,0,0q�6E   $	��Top=�6i   $	��110��6�   �� 5   
   -   5         ��Name$ btnClose��5   \   $��Đóng��6   $��22��6    $
��Left=�6,   $��607,506,0,0q�6G   $	��Top=�6n   $��90��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      	 �� 
TargetType"	 ��6.   .R�5   "   �� 
 ��Name$
 border��6)   $��1q�69    �� Padding$	 4,2q�6M   
������� �� CornerRadius$ 3��6r   #��z��6�   ._�5   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
 �/���6;   5      5      5      �� 5    
   -   5        $ btnOk��5    `   $��Cập Nhật��6   $��22��6$   $
��Left=�60   $��476,506,0,0q�6K   $	��Top=�6o   $��90��6�   ��5!      �� 5"      " ��6   .�5#      ��  � �� 5$      �� 5%      "	 ��6.   .R�5&   "   �� $
 border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�5'   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5(   #   5)      5+       5,      P� 5-       �@J�IsMouseOver . . �5.      ��  � ��
 �/���6;   5/      51      53      �� 54   
   -   5        $��lblNhap��54   "   $��Load...��6   $��Center=�6U   $��Center=�6y   $��339,6,255,502q�6�   $	��112��6�   ��55      �� 56      " ��6   .�57      ��  � ��
 ��  �63   �� 58       � ��$ Comic Sans MS)�63   �� 59       � ��$
 Bold"�63   �� 5:       � ��$ Center=�6<    5;      P� 5<       �@J�IsMouseOver . . �5=      ��  � ��
 � � �6;   �� 5>       � ��$
 Bold"�6;   5?      5A      5C      &   System.Windows.Controls.DataGrid  5D   
   -   5          Name$ dgvData��5D   �      SelectionMode$ Single=�6+      AutoGenerateColumns .  $	��457��6_      CanUserAddRows .     CanUserDeleteRows .  $
��Left=�6�   $��	10,44,0,0q�6�   $	��Top=�6�   $	��696��6�   ��6   ��    	CellStyle 5E      �� 5F      *  $System.Windows.Controls.DataGridCell"  6    5G      �� 5H       �� Binding 6&   �� *INDAM+ �� Value BOLD6@   .a�5I      ��  � ��
 ��  �6;   �� 5J       � ��$
 Bold"�6;   5K      5M         Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$ TT��6%      MinWidth$ 40Q�6M      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *	KT_KHOA+   HeaderStyle 5R      �� 5S      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5T      ��  � ��$ Center=�6K   5U         ElementStyle 5W      �� 5X      " ��6    .�5Y      ��  �@��TextAlignment$ Right=�6>   �� 5Z       � ��$ Center=�6B   �� 5[       � {� . 6:   5\      5^        5_      $	 STT��6%   $ 40Q�6P    .   62   �� *	KT_STT_HT+ 5`      �� 5a      "  6    .�5b      ��  � ��$ Center=�6K   5c       5e      �� 5f      " ��6    .�5g      ��  �@��TextAlignment$ Right=�6>   �� 5h       � ��$ Center=�6B   �� 5i       � {� . 6:   5j      5l        5m      $ Đối tượng��6%       Width5  /System.Windows.Controls.DataGridLengthConverter$  * 6S    .   68   �� *KT_DKT+ 5n      �� 5o      "  6    .�5p      ��  � ��$ Center=�6K   5q       5s      �� 5t      " ��6    .�5u      ��  �@��TextAlignment$
 Left=�6>   �� 5v       � ��$ Center=�6B   �� 5w       � {� . 6:   5x      5z        5{      $ Fonts Style��6%   $  80 6T    .   6:   �� *INDAM+ 5|      �� 5}      "  6    .�5~      ��  � ��$ Center=�6K   5       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   �� 5�       � {� . 6:   5�      5�        5�      $ Mức��6%   $  80 6O    .   62   �� *
KT_CAPHT+ 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   �� 5�       � {� . 6:   5�      5�        5�      $ Nhập��6%   $  80 6O    .   63   �� *	KT_AUTH+ 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  �@��TextAlignment$ Center=�6>   �� 5�       � ��$ Center=�6B   �� 5�       � {� . 6:   5�      5�      5�      5�      5�      