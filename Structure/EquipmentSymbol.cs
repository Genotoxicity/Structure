using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ProELib;

namespace Structure
{
    public class EquipmentSymbol
    {
        private Equipment equipment;
        private E3Font font;
        private E3Font pinFont;
        private double nameLength;
        private double width;
        private double height;
        private double pinWidth;
        private double topPinsHeight;
        private double bottomPinsHeight;


        private List<PinSymbol> topPins;
        private List<PinSymbol> bottomPins;

        public EquipmentSymbol(E3Project project, Equipment equipment)
        { 
            this.equipment = equipment;
            font = new E3Font(height: 3.5);
            pinFont = new E3Font(height: 2.5);
            topPins = new List<PinSymbol>();
            bottomPins = new List<PinSymbol>();
            foreach (PinInfo pinInfo in equipment.PinInfos)
            {
                if (pinInfo.Type == ConnectionType.In)
                    topPins.Add(new PinSymbol(project, pinInfo, pinFont));
                else
                    bottomPins.Add(new PinSymbol(project, pinInfo, pinFont));
            }
            Calculate(project);
        }

        private void Calculate(E3Project project)
        {
            pinWidth = GetJustifiedLength(pinFont.height);
            nameLength = project.E3Text.GetTextLength(equipment.Name, font);
            double justifiedNameLength = GetJustifiedLength(nameLength);
            double pinsWidth = Math.Max(topPins.Count, bottomPins.Count) * pinWidth;
            width = Math.Max(justifiedNameLength, pinsWidth);
            double topPinsMaxNameLength = topPins.Count > 0 ? topPins.Max(p => p.NameLength) : 0;
            double bottomPinsMaxNameLength = bottomPins.Count > 0 ? bottomPins.Max(p => p.NameLength) : 0;
            topPinsHeight = GetJustifiedLength(topPinsMaxNameLength);
            bottomPinsHeight = GetJustifiedLength(bottomPinsMaxNameLength);
            double justifiedNameHeight = GetJustifiedLength(font.height);
            height = topPinsHeight + bottomPinsHeight + justifiedNameHeight;
        }

        public void Place(E3Project project, Sheet sheet, int sheetId, Point leftBottom)
        {
            Graphic graph = project.Graphic;
            E3Text text = project.E3Text;
            List<int> ids = new List<int>();
            double left = leftBottom.X;
            double bottom = leftBottom.Y;
            double top = sheet.MoveUp(bottom, height);
            double right = sheet.MoveRight(left, width);
            ids.Add(graph.CreateRectangle(sheetId, left, bottom, right, top));
            double textX = (left + right) / 2;
            double textY = sheet.MoveDown((top + bottom) / 2, font.height / 2);
            int textId = graph.CreateText(sheetId, equipment.Name, textX, textY);
            text.Id = textId;
            text.SetFont(font);
            ids.Add(textId);
            if (topPins.Count>0)
            {
                double topWidth = topPins.Count * pinWidth;
                double topLeft = sheet.MoveRight(left, (width-topWidth) / 2);
                double topBottom = sheet.MoveDown(top, topPinsHeight);
                foreach(PinSymbol pinSymbol in topPins)
                {
                    double topRight = sheet.MoveRight(topLeft, pinWidth);
                    ids.Add(graph.CreateRectangle(sheetId, topLeft, top, topRight, topBottom));
                    textX = sheet.MoveRight((topRight+topLeft)/2, pinFont.height / 2);
                    textY = (top + topBottom) / 2;
                    textId = graph.CreateVerticalText(sheetId, pinSymbol.Name, textX, textY);
                    text.Id = textId;
                    text.SetFont(pinFont);
                    ids.Add(textId);
                    topLeft = topRight;
                }
            }
            if (bottomPins.Count > 0)
            {
                double bottomWidth = bottomPins.Count * pinWidth;
                double bottomLeft = sheet.MoveRight(left, (width-bottomWidth) / 2);
                double bottomTop = sheet.MoveUp(bottom, bottomPinsHeight);
                foreach (PinSymbol pinSymbol in bottomPins)
                {
                    double bottomRight = sheet.MoveRight(bottomLeft, pinWidth);
                    ids.Add(graph.CreateRectangle(sheetId, bottomLeft, bottomTop, bottomRight, bottom));
                    textX = sheet.MoveRight((bottomRight + bottomLeft) / 2, pinFont.height / 2);
                    textY = (bottomTop + bottom) / 2;
                    textId = graph.CreateVerticalText(sheetId, pinSymbol.Name, textX, textY);
                    text.Id = textId;
                    text.SetFont(pinFont);
                    bottomLeft = bottomRight;
                }
            }
        }

        private double GetJustifiedLength(double value)
        {
            return (int)(value + 3);
        }

    }
}
