   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfKhaoSat   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   �   -   5          � Title$6  0Chương Trình khảo sát hiệu quả cho vay��5   	    � Icon$ Icons/VBSP.ico��68   $��311.8��6N   $	��654��6]    � WindowStartupLocation$ CenterScreen=�6i   .��5      � .+�5   
   �� -   5        $��BtnClose��5   ]   $��Close��6   $��28��6!   $
��Left=�6-   $��342,231,0,0q�6H   $	��Top=�6m   $��96��6�   6�   �� 5   
   -   5        $��BtnOk��5   Z   $��OK��6   $��28��6   $
��Left=�6*   $��224,231,0,0q�6E   $	��Top=�6g   $��99��6   6�   �� 5   
   -   5        $��CboPos��5   N   $��22��6   $
��Left=�6   $��
184,15,0,0q�6:   $	��Top=�6\   $	��152��6t   $  Telerik.Windows.Controls.Label  5	   
   -   5        $��LblPos��5	   a   $	��POS��6   $��21��6&   $
��Left=�62   $��
153,15,0,0q�6M   $	��Top=�6o   $��38��6�     5
   
   -   5        $��LblNgay��5
   `   $��Ngày��6   $��25��6'   $
��Left=�63   $��7,14,0,0q�6N   $	��Top=�6o   $��58��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dtpNgay��5   O   $��21��6   $
��Left=�6!   $��	47,15,0,0q�6<   $	��Top=�6^   $	��102��6v   �� 5   
   -   5        $��GroupBox��5   N   $	��159��6   $
��Left=�6    $��	12,56,0,0q�6;   $	��Top=�6^   $	��622��6v   .��5      � $��
0,0,-2.8,0q�6    � ColumnDefinitions 5      �� 5      $
��334* �6'   �� 5      $
��Auto �6'   .+�5      	� -	   5        $��Ration01��5   �   $��0��6   $*��$Nhập số liệu khảo sát (M01)��6.   $
��auto��6T   $��17��6a   $
��Left=�6m   $��6,14,0,0q�6�   $	��Top=�6�   6�   	� 5      -
   5        $��Ration02��5   �   $��0��6   $-��'Báo cáo kết quả khảo sát (M02)��6.   $
��auto��6W   $��17��6d   $
��Left=�6p   $��6,35,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration03��5   �   $��0��6   $,��&BC khảo sát HQĐT hộ nghèo (M03)��6.   $
��auto��6X   $��16��6e   $
��Left=�6q   $��6,54,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration04��5   �   $��0��6   $2��,BC khảo sát HQĐT hộ cận nghèo (M04)��6.   $
��auto��6\   $��16��6i   $
��Left=�6u   $��6,73,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration05��5   �   $��0��6   $.��(BC khảo sát HQĐT hộ SXKD VKK (M05)��6.   $
��auto��6[   $��16��6h   $
��Left=�6t   $��6,91,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration06��5   �   $��0��6   $-��'BC khảo sát HQĐT cho vay DTTS (M06)��6.   $
��auto��6\   $��16��6i   $
��Left=�6u   $��	6,111,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration07��5   �   $��0��6   $/��)BC khảo sát HQĐT Cho vay NSVSMT (M07)��6.   $
��auto��6^   $��16��6k   $
��Left=�6w   $��	6,130,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration08��5   �   $��0��6   $-��'BC khảo sát HQĐT Cho vay HSSV (M08)��6.   $
��auto��6\   $��16��6i   $
��Left=�6u   $��
295,15,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration09��5   �   $��0��6   $.��(BC khảo sát HQĐT Cho vay XKLĐ (M09)��6.   $
��auto��6\   $��16��6i   $
��Left=�6u   $��
295,35,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration10��5   �   $��0��6   $-��'BC khảo sát HQĐT Cho vay GQVL (M10)��6.   $
��auto��6\   $��16��6i   $
��Left=�6u   $��
295,56,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration11��5   �   $��0��6   $9��3BC khảo sát HQĐT Cho vay Chòi tránh lũ (M11)��6.   $
��auto��6e   $��16��6r   $
��Left=�6~   $��
295,72,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration12��5   �   $��0��6   $;��5BC khảo sát HQĐT Cho vay Thương nhân VKK (M12)��6.   $
��auto��6g   $��16��6t   $
��Left=�6�   $��
295,91,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration13��5   �   $��0��6   $@��:BC khảo sát HQĐT Cho vay Nhà ở QĐ 167,QĐ 33 (M13)��6.   $
��auto��6j   $��16��6w   $
��Left=�6�   $��295,112,0,0q�6�   $	��Top=�6�   6�   	� 5      -   5        $��Ration14��5   �   $��0��6   $;��5BC khảo sát HQĐT Cho vay hộ thoát nghèo (M14)��6.   $
��auto��6f   $��16��6s   $
��Left=�6   $��295,133,0,0q�6�   $	��Top=�6�   6�   5       5!      �� 5"   
   $��Export��6   $
��Left=�6#   $��46��6>   $��	371,0,0,0q�6J   $	��Top=�6]   $	��152��6u   .��5#      � $��0,0,-2,0q�6    5$      �� 5%      $��9* �6'   �� 5&      $��2* �6'   .+�5(      	� -   5        $
��Opt1��5(   +   $��In��6   $
��Left=�67   $��21��6R   $��18,3,0,0q�6^   $	��Top=�6p   $��32��6�   	� 5)      -   5        $
��Opt2��5)   .   $��Excel��6   $
��Left=�6:   $��21��6U   $��67,3,0,0q�6a   $	��Top=�6s   $��59��6�   $��2��6�   $��0��6�    d� 	IsChecked$
 TrueY�6�   5+      5,      5-      5.      