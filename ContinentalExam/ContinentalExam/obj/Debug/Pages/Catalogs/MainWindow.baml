   M S B A M L   `   `   `  ����  ContinentalExam     ContinentalExam.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Products Catalog��5   	   $	��720��6"   $
��1080��6/    � WindowStartupLocation$ CenterScreen=�6<   .��5      � $
��1080��6   $	��720��6   .+�5   
   � $
��Left=�6   $��70��6*   $	��Top=�66   $
��1080��6N   ���#FFC000.+�5      �� $
��Left=�6   $��70��6/   $	��Top=�6;   $	��196��6S   $%��../../Resources/Images/Logo.png��6_   $
��Fill=�6�   �� 5      $��Products Catalog��6   $��0,0,5,0q�6/   $��70��6@   $
��Bold"�6L   $��34(�6^   $��Center=�6l   5	      � 5
   
   $
��Left=�6   $	��100��6*   $	��Top=�67   $
��1080��6O   $��0,70,0,0q�6\   .+�5      �� -   5        $��
Lbl_Search��5      $��Search:��6'   $
��Left=�69   $��	30,37,0,0q�6T   $	��Top=�6g   �� 5      -   5        $��
Txt_Search��5      $
��Left=�6(   $��23��6C   $��	83,40,0,0q�6O    �� TextWrapping$
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��442��6�   6�   �� 5      -   5        $��Btn_Add��5      $��35��6$   $��35��6/   $��1014,55,31,10q�6;   .��5      �� -   5        $��Img_Add��5      $$��../../Resources/Images/add.png��6'   $��26��6O   $��26��6Z    � ToolTip$ Add Product��6g   5      �� 5      -   5        $��
Btn_Search��5      $��Search��6'   $
��Left=�68   $��
543,40,0,0q�6S   $	��Top=�6g   $��75��6    J� RenderTransformOrigin$ 	0.053,0.2&�6�   6�   �� 5      -   5         ��Name$ 	Btn_Clean��5      $��Clean��6(   $
��Left=�68   $��
633,40,0,0q�6S   $	��Top=�6g   $��75��6   $ 	0.053,0.2&�6�   6�   5      � 5   
   $
��Left=�6   $	��520��6*   $	��Top=�67   $
��1080��6O   $��	0,170,0,0q�6\   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��Dgr_Products��5      $
��Left=�6+   $��10,0,0,0q�6F   $	��Top=�6X   $	��496��6p   $
��1040��6}      AutoGenerateColumns .     AlternatingRowBackground �#FFDC65   SelectionMode$ Single=�5   :   	   
IsReadOnly	 . 
   RowStyle
 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5   "   ��  �� Property � �� �� Value
 �   �6?   �� 5   "    � ��$
 Bold"�6?   5      5         Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ Product Description��6E      Width5  /System.Windows.Controls.DataGridLengthConverter$	 350 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *	Product+6b     5"      $
 Code��6B   $ 75 6P    6)   �� *Code+6P     5#      $ Price��6C   $ 75 6R    6)   �� *Price+6R     5$      $ Imported��6F   $ 75 6X    6)   �� *
Imported+6X     5%      $ Product Type��6U   $	 250 6k    6)   �� *ProductType.ProductType+6k     5&      $ Product Tax��6M   $ 75 6b    6)   �� *ProductType.Tax+6b   4  .System.Windows.Controls.DataGridTemplateColumn  5'      $	 120 6-      CellTemplate 5(      �� 5)      .R�5*   "   �� $��
Btn_Editar��6)   -   5        $��30��5*   ;   .��5+   &   �� $��
Img_Editar��6,   $%��../../Resources/Images/edit.png��6>   $ Edit Product��6g   5,   #   5-      5/      51      52      53      54      