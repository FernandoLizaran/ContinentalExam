   M S B A M L   `   `   `  ����  ContinentalExam.    (ContinentalExam.Pages.Transactions.Sales   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Sales��5   	   $	��789��6   $
��1080��6$    � WindowStartupLocation$ CenterScreen=�61   .��5      � $
��1080��6   $��0,6,-8,3q�6   .+�5   
   � $
��Left=�6   $��70��6*   $	��Top=�66   $
��1080��6N   ���#FFC000.+�5      �� $
��Left=�6   $��70��6/   $	��Top=�6;   $	��196��6S   $%��../../Resources/Images/Logo.png��6_   $
��Fill=�6�   �� 5      $��Sales��6   $��0,0,5,0q�6$   $��70��65   $
��Bold"�6A   $��34(�6S   $��Center=�6a   5	      � 5
   
   $
��Left=�6   $	��100��6*   $	��Top=�67   $
��1080��6O   $��0,70,0,0q�6\   .+�5      �� -   5        $��Lbl_PoductCode��5      $��Product Code:��6+   $
��Left=�6C   $��	30,14,0,0q�6^   $	��Top=�6q   �� 5      -   5        $��Txt_ProductCode��5      $
��Left=�6-   $��23��6H   $��
120,17,0,0q�6T    �� TextWrapping$
 Wrap=�6h   $�� ��6|   $	��Top=�6�   $	��191��6�   6�   �� 5      -   5         ��Name$ Lbl_Product��5      $�� ��6*   $
��Left=�65   $��
325,14,0,0q�6P   $	��Top=�6d   $	��239��6|   $
��Bold"�6�   �� 5      -   5        $ Lbl_Quantity��5      $��	Quantity:��6+   $
��Left=�6?   $��	30,45,0,0q�6Z   $	��Top=�6m    J� RenderTransformOrigin$ 0.828,0.769&�6�   �� 5      -   5         ��Name$ Txt_Quantity��5      $
��Left=�6,   $��23��6G   $��
120,48,0,0q�6S   $
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��191��6�   6�   �� 5      -   5        $��Btn_Add��5      $	��Add��6$   $
��Left=�62   $��
325,48,0,0q�6M   $	��Top=�6a   $��75��6y   $ 	0.053,0.2&�6�   6�   �� 5      -   5         ��Name$ 
Btn_Search��5      $��Search Product��6)   $
��Left=�6B   $��
416,48,0,0q�6]   $	��Top=�6q   $	��102��6�   $ 	0.053,0.2&�6�   6�   �� 5      -   5        $ 	Btn_Clean��5      $��Clean��6(   $
��Left=�68   $��
535,48,0,0q�6S   $	��Top=�6g   $��75��6   $ 	0.053,0.2&�6�   6�   �� 5      -	   5        $ Btn_ProductsCatalog��5      $��Product Catalog��62   $
��Left=�6L   $��	915,0,0,0q�6g   $	��Top=�6z   $	��165��6�   $ 	0.053,0.2&�6�   6�   5      � 5   
   $
��Left=�6   $	��569��6*   $	��Top=�67   $
��1080��6O   $��	0,170,0,0q�6\   .+�5      &   System.Windows.Controls.DataGrid  -
   5        $��	Dgr_Sales��5      $
��Left=�6(   $��10,0,0,0q�6C   $	��Top=�6U   $	��496��6m   $
��1040��6z      AutoGenerateColumns .     AlternatingRowBackground �#FFDC65	   SelectionMode$	 Single=�5   :   
   
IsReadOnly
 .    RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5   "   ��  �� Property � �� �� Value
 �   �6?   �� 5   "    � ��$
 Bold"�6?   5      5!         Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn   Header$
 Code��6J      Width5  /System.Windows.Controls.DataGridLengthConverter$ 75 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Product.Code+6X     5%      $ Product Description��6M   $	 350 6j    6)   �� *Product.Product+6j     5&      $ Quantity��6F   $ 75 6X    6)   �� *
Quantity+6X     5'      $ Price��6K   $ 75 6Z    6)   �� *Product.Price+6Z     5(      $ Total��6C   $ 75 6R    6)   �� *Total+6R     5)      $
 Tax ��6A   $ 75 6O    6)   �� *Tax+6O     5*      $ Imported Tax ��6I   $	 100 6`    6)   �� *ImportedTax+6`     5+      $ 
Round Tax ��6F   $ 75 6Z    6)   �� *
FinalTax+6Z   4  .System.Windows.Controls.DataGridTemplateColumn  5,      $	 120 6-      CellTemplate 5-      �� 5.      .R�5/   "   �� $��
Btn_Delete��6)   -   5        $��30��5/   ;   .��50   &   �� $��
Img_Delete��6,   $'��!../../Resources/Images/delete.png��6>    � ToolTip$ Delete Product��6i   51   #   52      54      56      �� 57      -   5        $��	Lbl_Total��57      $��Total Sale: 0��6%   $��Right=�6=   $��Right=�6Y   $
��Bold"�6|   $��
0,496,30,0q�6�   $	��Top=�6�   $	��140��6�   �� 58      -   5        $��Btn_CancelSale��58      $��Cancel Sale��6+   $
��Left=�6A   $��950,539,0,0q�6\   $	��Top=�6q   $	��100��6�   6�   �� 59      -   5        $ 
Btn_Finish��59      $��Finish Sale��6)   $
��Left=�6?   $��835,539,0,0q�6Z   $	��Top=�6o   $	��100��6�   6�   5:      5;      5<      