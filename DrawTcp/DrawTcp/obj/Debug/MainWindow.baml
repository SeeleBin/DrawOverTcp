   M S B A M L   `   `   `  ����B  >DrawTcp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DrawTcpDrawTcp  5   	       DrawTcp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:DrawTcp     5   	     � Title$  DrawOverTCP��5   	   $	��482��6   $	��808��6*    � 
ResizeMode$ NoResize=�66   .��5	      � -   5         �Name$ MainGrid��5	      $��	0,0,4,-19q�6    � RowDefinitions 5
   
   �� 5      $
��308* �6   �� 5      .+�5   
   �� -   5         ��Name$ drawingCanvas��5      $
��Left=�6(   $	��419��6C   $	��Top=�6P   $	��538��6h   $��
256,23,0,0q�6t   
�������6�   �� 5   
   $��Insert your guess here!��6   $
��Left=�62   $	��Top=�6M   $��10,5,0,0q�6e   $	��210��6w   $��39��6�   $��20(�6�   �� 5   
   -   5         ��Name$ GuessTxt��5      $
��Left=�6$   $��33��6?    �� TextWrapping$
 Wrap=�6K   $	��Top=�6_   $	��210��6w   $��
10,377,0,0q�6�   $��20(�6�   �� 5   
   -   5         ��Name$ OldwordsTxt��5      $
��Left=�6'   $	��328��6B   $	��Top=�6O   $	��210��6g   $��	10,44,0,0q�6s   �� 5   
   -   5         ��Name$ checkAnswBtn��5      $��Check you answer!��6'   $
��Left=�6C   $��
10,415,0,0q�6^   $	��Top=�6r   $	��210��6�   $��32��6�   6�   5      5      