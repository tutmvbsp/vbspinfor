   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfExcel   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Export Excel��5   	   $	��522��6   $	��747��6+    � WindowStartupLocation$ CenterScreen=�67    � Icon$ Icons/Cryoword.ico��6\   .��5      � .+�5   
   �� -   5        $��btnClose��5   ]   $��Close��6   $��28��6!   $
��Left=�6-   $��583,440,0,0q�6H   $	��Top=�6m   $��81��6�   6�   �� 5   
   -   5        $��btnRead��5   \   $
��Read��6   $��28��6    $
��Left=�6,   $��381,440,0,0q�6G   $	��Top=�6k   $��81��6�   6�   �� 5   
   -   5        $��btnWrite��5   ^   $��Export��6   $��28��6"   $
��Left=�6.   $��479,440,0,0q�6I   $	��Top=�6n   $��81��6�   6�   &   System.Windows.Controls.DataGrid  5	   
   -   5        $��dgvData��5	   i      AutoGenerateColumns . $	��372��6.   $
��Left=�6;   $��	24,48,0,0q�6V   $	��Top=�6x   $	��679��6�   �� 5
   
   -   5        $��lblSheet��5
   b   $��
Sheet Name��6   $
��auto��6%   $
��Left=�63   $��
455,12,0,0q�6N   $	��Top=�6r   $
��auto��6�   �� 5   
   -   5        $��CboSheet��5   N   $��21��6   $��Right=�6   $��	0,15,48,0q�6;   $	��Top=�6^   $	��106��6v   �� 5   
   -   5        $��	btnBrowse��5   `   $��	Browse...��6   $��22��6%   $
��Left=�61   $��
338,15,0,0q�6L   $	��Top=�6q   $��82��6�   6�   �� 5   
   -	   5        $��txtPath��5   L   $��27��6   $
��Left=�6   $��	24,14,0,0q�69   $	��Top=�6[   $	��299��6s   �� 5   
   -
   5         ��Name$ grpChon��5   a   $��	File Type��6   $��55��6&   $
��Left=�62   $��
24,426,0,0q�6M   $	��Top=�6r   $	��214��6�   .��5      �  � ColumnDefinitions 5      �� 5      $	��64* �6'   �� 5      $	��11* �6'   .+�5      	� -   5         	�Name$ Ration1��5   u   $	��xls��6    	� 	GroupName$ G1��6,   $��15��6<   $
��Left=�6H   $��21,6,0,0q�6c   $	��Top=�6�   $��62��6�   	� 5      -   5        $ Ration2��5   v   $
��xlsx��6   $ G1��6-   $��15��6=   $
��Left=�6I   $��87,6,0,0q�6d   $	��Top=�6�   $��84��6�   5      5      5      5      