using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace InShadows
{
    class Editor
    {
        Entity editEntity;
        Entity top, left, bottom, right;

        MouseState prevMouseState, currMouseState;
        KeyboardState prevKeyState, currKeyState;
        Level currLevel;
        bool isDragging = false;
        bool isResizingTop = false;
        bool isResizingBottom = false;
        bool isResizingLeft = false;
        bool isResizingRight = false;
        int outlineSize = 7;

        public Editor()
        {
            editEntity = null;
            UpdateKeyState();
            UpdateMouseState();

            bottom = new Entity(0, 0, outlineSize, outlineSize);
            bottom.Color = Color.DarkOrange;
            left = new Entity(0, 0, outlineSize, outlineSize);
            left.Color = Color.DarkOrange;
            top = new Entity(0, 0, outlineSize, outlineSize);
            top.Color = Color.DarkOrange;
            right = new Entity(0, 0, outlineSize, outlineSize);
            right.Color = Color.DarkOrange;
        }

        public Level CurrentLevel
        {
            get { return currLevel; }
            set { currLevel = value; }
        }

        public void Draw(Renderer2D renderer)
        {
            renderer.BeginScene(currLevel.Camera.get_transformation());
            renderer.DrawEntity(top);
            renderer.DrawEntity(left);
            renderer.DrawEntity(bottom);
            renderer.DrawEntity(right);
            renderer.EndScene();
        }

        public void Update()
        {
            UpdateKeyState();
            UpdateMouseState();
            //TODO check if user has mouse over Editor GUI first
            if (currLevel != null)
            {
                if (editEntity != null)
                {
                    if (currMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed)
                    {
                        isDragging = true;
                        Vector2 currMouse = currLevel.Camera.screenToWorld(new Vector2(currMouseState.X, currMouseState.Y));
                        
                        isResizingTop       = top.containsPoint((int)currMouse.X, (int)currMouse.Y);
                        isResizingBottom    = bottom.containsPoint((int)currMouse.X, (int)currMouse.Y);
                        isResizingLeft      = left.containsPoint((int)currMouse.X, (int)currMouse.Y);
                        isResizingRight     = right.containsPoint((int)currMouse.X, (int)currMouse.Y);

                    }

                    if (isDragging)
                    {
                        if (currMouseState.LeftButton != ButtonState.Pressed)
                        {
                            isDragging = false;
                            isResizingTop = false;
                            isResizingBottom = false;
                            isResizingLeft = false;
                            isResizingRight = false;
                        }
                        else
                        {
                            Vector2 prevMouse = currLevel.Camera.screenToWorld(new Vector2(prevMouseState.X, prevMouseState.Y));
                            Vector2 currMouse = currLevel.Camera.screenToWorld(new Vector2(currMouseState.X, currMouseState.Y));

                            if (!(isResizingTop || isResizingRight || isResizingLeft || isResizingBottom))
                            {
                                editEntity.move((int)(currMouse.X - prevMouse.X), (int)(currMouse.Y - prevMouse.Y));
                            }
                            else
                            {
                                if (isResizingRight)
                                {
                                    editEntity.resize((int)(currMouse.X - prevMouse.X), 0);
                                }

                                if (isResizingTop)
                                {
                                    editEntity.resize(0, (int)(currMouse.Y - prevMouse.Y));
                                }

                                if (isResizingLeft)
                                {
                                    editEntity.move((int)(currMouse.X - prevMouse.X), 0);
                                    editEntity.resize((int)(prevMouse.X - currMouse.X), 0);
                                    
                                }

                                if (isResizingBottom)
                                {
                                    editEntity.move(0, (int)(currMouse.Y - prevMouse.Y));
                                    editEntity.resize(0, (int)(prevMouse.Y - currMouse.Y));

                                }
                            }
                        }
                    }
                }

                if (!isDragging)
                {
                    editEntity = currLevel.getEntityAt(currMouseState.X, currMouseState.Y);
                }
                buildEntityBounds();
                checkKeyboard();
            }
        }

        protected void buildEntityBounds()
        {
            if (editEntity == null)
            {
                top.IsInvisible = true;
                bottom.IsInvisible = true;
                left.IsInvisible = true;
                right.IsInvisible = true;
                return;
            }

            bottom.moveTo(editEntity.Bounds.Left, editEntity.Bounds.Top);
            bottom.setSize(editEntity.Bounds.Width, outlineSize);

            left.moveTo(editEntity.Bounds.Left, editEntity.Bounds.Top);
            left.setSize(outlineSize, editEntity.Bounds.Height);

            top.moveTo(editEntity.Bounds.Left, editEntity.Bounds.Bottom - outlineSize);
            top.setSize(editEntity.Bounds.Width, outlineSize);

            right.moveTo(editEntity.Bounds.Right - outlineSize, editEntity.Bounds.Top);
            right.setSize(outlineSize, editEntity.Bounds.Height);

            top.IsInvisible = false;
            bottom.IsInvisible = false;
            left.IsInvisible = false;
            right.IsInvisible = false;
        }

        protected void checkKeyboard()
        {
            if (currKeyState.IsKeyDown(Keys.N) && prevKeyState.IsKeyUp(Keys.N))
            {
                Vector2 currMouse = currLevel.Camera.screenToWorld(new Vector2(currMouseState.X, currMouseState.Y));
                Entity newEnt = new Entity((int)currMouse.X, (int)currMouse.Y, 50,50);
                newEnt.Color = Color.Aqua;
                currLevel.addEntity(newEnt);
            }
        }

        protected void UpdateMouseState()
        {
            prevMouseState = currMouseState;
            currMouseState = Mouse.GetState();
        }

        protected void UpdateKeyState()
        {
            prevKeyState = currKeyState;
            currKeyState = Keyboard.GetState();

        }

    }
}
