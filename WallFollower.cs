using System.Collections.Generic;
using System;
using SwinGameSDK;

namespace MyGame
{
    public class WallFollower : Enemy
    {
        private static Random _rand = new Random();
        private Tile _targetTile;
        private Tile _turnedAt;
        private Direction _wallSide;
        private Direction _direction;

        public WallFollower()
        {
            _targetTile = null;
            _turnedAt = null;
            _wallSide = Direction.Null;
            _direction = Direction.Null;
        }

        public override void Step()
        {
            if (_wallSide == Direction.Null)
            {
                _wallSide = GetWallSide();
            }

            if (_direction == Direction.Null)
            {
                _direction = GetDirection();
            }

            int X = (int)Math.Truncate(AbsPos.X);
            int Y = (int)Math.Truncate(AbsPos.Y);

            int tileX = 0;
            int tileY = 0;

            /// <summary>
            /// Store target Tile positions for use in determining collisions.
            /// </summary>
            if (_targetTile != null)
            {
                tileX = (int) Math.Truncate(_targetTile.AbsPos.X);
                tileY = (int) Math.Truncate(_targetTile.AbsPos.Y);
            }

            // If AI does not have a target Tile or has reached its target Tile,  get a new one.
            if (_targetTile == null || (X == tileX && Y == tileY))
            {
                if (GetWallSide() != Direction.Null)
                {
                    _targetTile = GetTargetTile();
                }
                else
                {
                    HandleDiagonalTile();
                }
            }

            if (_targetTile.IsWall)
            {
                if (_turnedAt != TileInteractor.TileAt(AbsPos.X, AbsPos.Y))
                {
                    _wallSide = _direction;
                    _direction = GetDirection();
                    _turnedAt = TileInteractor.TileAt(AbsPos.X, AbsPos.Y);
                }
            }

            /// <summary>
            /// Checks if the AI has reached its target Tile.
            /// MOVEMENT IS ALSO HANDLED HERE.
            /// </summary>
            if (_targetTile != null)
            {
                if (MoveToTile (_targetTile))
                {
                    _targetTile = null;
                }
            }
        }

        public Direction GetWallSide()
        {
            if (TileInteractor.TileAt(AbsPos.X - 1, AbsPos.Y).IsWall)
                return Direction.West;
            else if (TileInteractor.TileAt(AbsPos.X, AbsPos.Y - 1).IsWall)
                return Direction.North;
            else if (TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE + 1, AbsPos.Y).IsWall)
                return Direction.East;
            else if (TileInteractor.TileAt(AbsPos.X, AbsPos.Y + Renderer.TILE_SIZE + 1).IsWall)
                return Direction.South;
            else
                return Direction.Null;
        }

        /// <summary>
        /// Gets the new direction when a corner is encountererd. AI will move tangentally from wall side.
        /// Else's are redundant, but increase code clarity.
        /// </summary>
        public Direction GetDirection()
        {
            int coinFlip = _rand.Next(0, 1);

            if (_wallSide == Direction.North || _wallSide == Direction.South)
            {
                if (coinFlip == 0)
                {
                    if (!TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE + 1, AbsPos.Y).IsWall)
                        return Direction.East;
                    else
                        return Direction.West;
                }
                else
                {
                    if (!TileInteractor.TileAt(AbsPos.X - 1, AbsPos.Y).IsWall)
                        return Direction.West;
                    else
                        return Direction.East;
                }
            }
            else
            {
                if (coinFlip == 0)
                {
                    if (!TileInteractor.TileAt(AbsPos.X, AbsPos.Y + Renderer.TILE_SIZE + 1).IsWall)
                        return Direction.South;
                    else
                        return Direction.North;
                }
                else
                {
                    if (!TileInteractor.TileAt(AbsPos.X, AbsPos.Y - 1).IsWall)
                        return Direction.North;
                    else
                        return Direction.South;
                }
            }
        }

        /// <summary>
        /// Figures out which diagonal tile is a wall and moves and adjusts values accordingly.
        /// </summary>
        public void HandleDiagonalTile()
        {
            _wallSide = Direction.Null;

            Point2D tileIndex = TileInteractor.GetTileIndexFromPt(AbsPos);
            int col = (int)tileIndex.X;
            int row = (int)tileIndex.Y;

            // Consider South-East and South-West tiles
            if (_direction == Direction.North)
            {
                if (TileInteractor.Tileset.Tiles[col + 1][row + 1].IsWall) //South-East
                    _direction = Direction.East;
                else //South-West is wall
                    _direction = Direction.West;

                _targetTile = GetTargetTile();
                return;
            }

            //Consider North-West and South-West tiles
            if (_direction == Direction.East)
            {
                if (TileInteractor.Tileset.Tiles[col - 1][row - 1].IsWall) //North-West
                    _direction = Direction.North;
                else //South-West is wall
                    _direction = Direction.South;

                _targetTile = GetTargetTile();
                return;
            }

            //Consider North-East and North-West tiles
            if (_direction == Direction.South)
            {
                if (TileInteractor.Tileset.Tiles[col + 1][row - 1].IsWall) //North-East
                    _direction = Direction.East;
                else //North-West is wall
                    _direction = Direction.West;

                _targetTile = GetTargetTile();
                return;
            }

            //Consider North-East and South-East tiles
            if (_direction == Direction.West)
            {
                if (TileInteractor.Tileset.Tiles[col + 1][row - 1].IsWall) //North-East
                    _direction = Direction.North;
                else //South-East is wall
                    _direction = Direction.South;

                _targetTile = GetTargetTile();
                return;
            }
        }

        public Tile GetTargetTile()
        {
            if (_direction == Direction.North)
                return TileInteractor.TileAt(AbsPos.X, AbsPos.Y - Renderer.TILE_SIZE);
            else if (_direction == Direction.NorthEast)
                return TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE, AbsPos.Y - Renderer.TILE_SIZE);
            else if (_direction == Direction.East)
                return TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE, AbsPos.Y);
            else if (_direction == Direction.SouthEast)
                return TileInteractor.TileAt(AbsPos.X + Renderer.TILE_SIZE,  AbsPos.Y + Renderer.TILE_SIZE);
            else if (_direction == Direction.South)
                return TileInteractor.TileAt(AbsPos.X, AbsPos.Y + Renderer.TILE_SIZE);
            else if (_direction == Direction.SouthWest)
                return TileInteractor.TileAt(AbsPos.X - Renderer.TILE_SIZE, AbsPos.Y + Renderer.TILE_SIZE);
            else if (_direction == Direction.West)
                return TileInteractor.TileAt(AbsPos.X - Renderer.TILE_SIZE, AbsPos.Y);
            else
                return TileInteractor.TileAt(AbsPos.X - Renderer.TILE_SIZE, AbsPos.Y - Renderer.TILE_SIZE);
        }

        /// <summary>
        /// This method is redundant. Used it as a wrapper to keep consistent with Enemy inheritance tree.
        /// </summary>
        public override void GetPath()
        {
            GetTargetTile();
        }
    }
}