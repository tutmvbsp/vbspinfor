   M S B A M L   `   `   `  ����M  ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089M3clr-namespace:System.ComponentModel;assembly=SystemSystem.ComponentModel  5   	    Presentation   Presentation.WpfBack   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   Bcm3clr-namespace:System.ComponentModel;assembly=System     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$!  Backup and Restore database��5   	   $��209.463��6-   $��376.85��6>    � WindowStartupLocation$ CenterScreen=�6M    � Icon$ Icons/Cryoword.ico��6r   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��257* �6   �� 5	      $	��43* �6   �� 5
      $	��69* �6   .+�5   
   �� -   5        $��btnClose��5   ]   $��Close��6   $��20��6!   $
��Left=�6-   $��193,130,0,0q�6H   $	��Top=�6m   $��81��6�   $��2��6�   �� 5   
   -   5        $��btnOkNew��5   Y   $��OK��6   $��21��6   $
��Left=�6*   $��
96,130,0,0q�6E   $	��Top=�6i   $��81��6�   6�   �� 5   
   -   5        $��	btnBrowse��5   Z   $	��...��6   $��24��6   $
��Left=�6+   $��
202,72,0,0q�6F   $	��Top=�6k   $��19��6�    J� RenderTransformOrigin$ 21.194,-1.633&�6�   6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dtpNgay��5   O   $��24��6   $
��Left=�6!   $��	75,27,0,0q�6<   $	��Top=�6^   $	��100��6v   �� 5   
   -   5        $��lblNgay��5   \   $��Date ��6   $
��auto��6    $
��Left=�6.   $��	24,25,0,0q�6I   $	��Top=�6k   $
��auto��6�   �� 5   
   -   5        $��	lblTarGet��5   ^   $��Target ��6   $
��auto��6"   $
��Left=�60   $��	24,70,0,0q�6K   $	��Top=�6o   $
��auto��6�   �� 5   
   -   5        $��txtPath��5   `   $
��Left=�6   $��23��6-   $��	74,72,0,0q�69    �� TextWrapping$
 Wrap=�6L   $��C:\Tam\��6o   $	��Top=�6~   $	��123��6�   �� 5   
   -	   5        $��CboDb��5   r   $
��Left=�6   $��24��6.   $��
196,27,0,0q�6:   $	��Top=�6N   $	��133��6f   $��3��6   �� 5   
   -
   5        $��lblMess��5   }   $�� ��6   $
��Left=�6   $
��auto��66   $��
74,104,0,0q�6D   $	��Top=�6X   $
��auto��6p   6}   	� 5   
   -   5        $��Option1��5   �   $��3��6   $��Backup��6*   $
��Left=�6;   $
��auto��6V   $��
243,67,0,0q�6d   $	��Top=�6x   $��75��6�    d� 	IsChecked$
 TrueY�6�   	� 5   
   -   5        $��Option2��5   �   $��3��6   $��Restore��6*   $
��Left=�6<   $
��auto��6W   $��
243,83,0,0q�6e   $	��Top=�6y   $��75��6�   6�   5      5      