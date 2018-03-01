using System;
using System.Linq;
using System.Linq.Expressions;
using Xamarin.Forms;
using XamarinForm.Delegates;

namespace XamarinForm.Games
{
    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// 游戏初始
        /// </summary>
        Initialized,
        /// <summary>
        /// 游戏开始
        /// </summary>
        Start,
        /// <summary>
        /// 游戏暂停
        /// </summary>
        Stop,
        /// <summary>
        /// 游戏结束
        /// </summary>
        End
    }
    public class Board : AbsoluteLayout
    {
        #region 字段
        const string timeFormat = @"%m\:ss";
        Random random = new Random();
        /// <summary>
        /// 游戏状态
        /// </summary>
        GameStatus gameStatus = GameStatus.End;
        /// <summary>
        /// 标记数量
        /// </summary>
        int flaggedTileCount = 0;
        /// <summary>
        /// 未暴露的雷区
        /// </summary>
        int unExposedTile = 0;
        int mineCount = 0;
        /// <summary>
        /// 雷区
        /// </summary>
        Tile[,] tiles;
        DateTime startTime,endTime,beginStopTime;
        TimeSpan gameTime ,stopTime ;
        #endregion

        #region 属性
        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { get; private set; }
        /// <summary>
        /// 列数
        /// </summary>
        public int ColumnCount { get; private set; }
        /// <summary>
        /// 地雷数量
        /// </summary>
        public int MineCount
        {
            get { return mineCount; }
            private set
            {
                mineCount = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 游戏时间
        /// </summary>
        public TimeSpan GameTime
        {
            get { return gameTime; }
            private set
            {
                if (gameTime != value)
                {
                    gameTime = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// 暂停时间
        /// </summary>
        public TimeSpan StopTime
        {
            get { return stopTime; }
            private set
            {
                if (stopTime != value)
                {
                    stopTime = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// 游戏状态
        /// </summary>
        public GameStatus Status { get { return gameStatus; } private set { gameStatus = value; } }
        /// <summary>
        /// 标记地雷数量
        /// </summary>
        public int FlaggedTileCount
        {
            set
            {
                if (flaggedTileCount != value)
                {
                    flaggedTileCount = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return flaggedTileCount;
            }
        }
        /// <summary>
        /// 未暴露雷区
        /// </summary>
        public int UnExposedTile
        {
            set
            {
                if (unExposedTile != value)
                {
                    unExposedTile = value;
                    OnPropertyChanged();
                }
            }
            get
            {
                return unExposedTile;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 当游戏开始时触发
        /// </summary>
        public event MineGameHandle onGameStart;
        /// <summary>
        /// 当游戏胜利时触发
        /// </summary>
        public event MineGameHandle onVictory;
        /// <summary>
        /// 当游戏失败时触发
        /// </summary>
        public event MineGameHandle onDefeated;
        /// <summary>
        /// 当游戏暂停时触发
        /// </summary>
        public event MineGameHandle onStop;
        /// <summary>
        /// 当游戏继续时触发
        /// </summary>
        public event MineGameHandle onResume;
        #endregion

        #region 构造函数
        public Board()
        {

        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 重新开始游戏
        /// </summary>
        public void NewGame()
        {
            foreach (Tile tile in tiles)
            {
                tile.Initialize();
            }
            initializedProperty();
        }

        /// <summary>
        /// 初始化游戏
        /// </summary>
        /// <param name="RowCount"></param>
        /// <param name="ColumnCount"></param>
        /// <param name="MineCount"></param>
        public void Initialized(int RowCount, int ColumnCount, int MineCount)
        {
            initializedProperty();
            if (RowCount <= 0)
            {
                throw new Exception("行数不能小于等于0！");
            }
            if (ColumnCount <= 0)
            {
                throw new Exception("列数不能小于等于0！");
            }
            if (MineCount > RowCount * ColumnCount)
            {
                throw new Exception("地雷数量不能超出雷区数量！");
            }
            unExposedTile = RowCount * ColumnCount;
            Children.Clear();
            this.RowCount = RowCount;
            this.ColumnCount = ColumnCount;
            this.MineCount = MineCount;
            Status = GameStatus.Initialized;
            tiles = new Tile[this.RowCount, this.ColumnCount];

            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    Tile tile = new Tile(row, column);
                    tile.TileStatusChanged += Tile_TileStatusChanged;
                    tiles[row, column] = tile;
                    Children.Add(tile);
                }
            }

            double tileWidth = Width / ColumnCount, tileHeigh = Height / RowCount;
            foreach (Tile tile in tiles)
            {
                Rectangle rectangle = new Rectangle(tile.Column * tileWidth, tile.Row * tileHeigh, tileWidth, tileHeigh);
                SetLayoutBounds(tile, rectangle);
            }
        }
        /// <summary>
        /// 游戏暂停
        /// </summary>
        public void Stop()
        {
            Status = GameStatus.Stop;
            beginStopTime = DateTime.Now;
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                stopTime += DateTime.Now - beginStopTime;
                return Status == GameStatus.Stop;
            });
            if (onStop != null)
            {
                onStop.Invoke();
            }
        }
        /// <summary>
        /// 游戏继续
        /// </summary>
        public void Resume()
        {
            Status = GameStatus.Start;
            startTime = DateTime.Now;
            //Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            //{
            //    if (endTime != null&&endTime>startTime)
            //    {
            //        GameTime += endTime - startTime;
            //    }
            //    else
            //    {
            //        GameTime += DateTime.Now - startTime;
            //    }
            //    return Status == GameStatus.Start;
            //});
            if (onResume != null)
            {
                onResume.Invoke();
            }
        }
        #endregion

        #region 私有方法
        void Board_SizeChanged(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        void initializedProperty()
        {
            Status = GameStatus.Initialized;
            UnExposedTile = tiles != null ? tiles.Length : 0;
            FlaggedTileCount = 0;
            GameTime = new TimeSpan();
            StopTime = new TimeSpan();
        }

        /// <summary>
        /// 雷区状态改变时触发
        /// </summary>
        /// <param name="sender">雷区</param>
        /// <param name="e">状态</param>
        void Tile_TileStatusChanged(object sender, TileStatus e)
        {
            Tile tile = sender as Tile;
            if (Status == GameStatus.End) return;
            switch (e)
            {
                case TileStatus.Exposed:
                    if (Status == GameStatus.Initialized)
                    {
                        Status = GameStatus.Start;
                        startTime = DateTime.Now;
                        //使用设备时钟功能启动一个循环计时器。
                        Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
                        {
                            if (endTime != null&& endTime>startTime)
                            {
                                GameTime = endTime - startTime;
                            }
                            else
                            {
                                GameTime = DateTime.Now - startTime;
                            }
                            return Status != GameStatus.End;
                        });
                        
                        CreateMine();
                        //SetTileSurroundingMineCount();
                        Label label = tile.Content as Label;
                        label.Text = tile.SurroundingMineCount > 0 ? tile.SurroundingMineCount.ToString() : " ";

                        if (onGameStart != null)
                            onGameStart.Invoke();
                    }
                    if (!tile.IsMine)
                    {
                        if (tile.SurroundingMineCount == 0)
                        {
                            CycleThroughNeighbors(tile.Row, tile.Column, (row, column) =>
                            {
                                tiles[row, column].Status = TileStatus.Exposed;
                            });
                        }
                        //ExposeSurroundingTile(tile.Row, tile.Column);
                    }
                    else
                    {
                        endTime = DateTime.Now;
                        Status = GameStatus.End;
                        if (onDefeated != null)
                        {
                            onDefeated.Invoke();
                        }
                    }
                    break;
                case TileStatus.Hidden:
                    break;
                case TileStatus.Flagged:
                    break;
            }
            TileStatusCount();
            if (IsVictory())
            {
                endTime = DateTime.Now;
                Status = GameStatus.End;
                if (onVictory != null)
                {
                    onVictory.Invoke();
                }
            }
        }
        
        /// <summary>
        /// 是否已胜利
        /// </summary>
        /// <returns></returns>
        Boolean IsVictory()
        {
            //foreach (Tile tile in tiles)
            //{
            //    if (tile.IsMine && tile.Status != TileStatus.Flagged) return false;
            //    if (!tile.IsMine && tile.Status != TileStatus.Exposed) return false;
            //}
            //return true;
            return FlaggedTileCount == MineCount && FlaggedTileCount == unExposedTile;
        }

        /// <summary>
        /// 计算未暴露雷区及标记雷区数量
        /// </summary>
        void TileStatusCount()
        {
            int _flaggedTileCount = 0, _unExposed = 0;
            foreach (Tile tile in tiles)
            {
                if (tile.Status == TileStatus.Flagged)
                {
                    _flaggedTileCount++;
                }

                if (tile.Status != TileStatus.Exposed)
                {
                    _unExposed++;
                }
            }

            FlaggedTileCount = _flaggedTileCount;
            UnExposedTile = _unExposed;
        }

        /// <summary>
        /// 暴露周围雷区
        /// </summary>
        /// <param name="rowIndex">行下标</param>
        /// <param name="columnIndex">列下标</param>
        void ExposeSurroundingTile(int rowIndex, int columnIndex)
        {
            for (int _rowIndex = rowIndex - 1; _rowIndex <= rowIndex + 1; _rowIndex++)
            {
                if (_rowIndex < 0) continue;
                if (_rowIndex >= RowCount) continue;
                for (int _columnIndex = columnIndex - 1; _columnIndex <= columnIndex + 1; _columnIndex++)
                {
                    if (_columnIndex < 0) continue;
                    if (_columnIndex >= ColumnCount) continue;
                    if (_rowIndex == rowIndex && _columnIndex == columnIndex) continue;

                    tiles[_rowIndex, _columnIndex].Status=TileStatus.Exposed;
                    if (tiles[_rowIndex, _columnIndex].SurroundingMineCount == 0)
                        ExposeSurroundingTile(_rowIndex, _columnIndex);
                }
            }
        }

        /// <summary>
        /// 创建地雷
        /// </summary>
        void CreateMine()
        {
            int _mineCount = 0;
            while (_mineCount < MineCount)
            {
                int rowIndex= random.Next(RowCount);
                int columnIndex= random.Next(ColumnCount);
                while (tiles[rowIndex, columnIndex].IsMine)
                {
                    rowIndex = random.Next(RowCount);
                    columnIndex = random.Next(ColumnCount);
                }
                tiles[rowIndex, columnIndex].IsMine = true;
                CycleThroughNeighbors(rowIndex, columnIndex, (row, column) =>
                {
                    ++tiles[row, column].SurroundingMineCount;
                });
                _mineCount++;
            }
        }

        /// <summary>
        /// 设置雷区周围地雷数量
        /// </summary>
        void SetTileSurroundingMineCount()
        {
            for (int _rowIndex = 0; _rowIndex < RowCount; _rowIndex++)
            {
                for (int _columnIndex = 0; _columnIndex < ColumnCount; _columnIndex++)
                {                    
                    if (tiles[_rowIndex, _columnIndex].IsMine)  continue;

                    tiles[_rowIndex, _columnIndex].SurroundingMineCount = GetTileSurroundingMineCount(_rowIndex, _columnIndex);
                }
            }
        }

        /// <summary>
        /// 获取指定坐标周围地雷数量
        /// </summary>
        /// <param name="rowIndex">行下标</param>
        /// <param name="columnIndex">列下标</param>
        /// <returns></returns>
        int GetTileSurroundingMineCount(int rowIndex, int columnIndex)
        {
            int _surroundingMineCount = 0;
            for (int _rowIndex = rowIndex - 1; _rowIndex <= rowIndex + 1; _rowIndex++)
            {
                if (_rowIndex < 0) continue;
                if (_rowIndex >= RowCount) continue;
                for (int _columnIndex = columnIndex - 1; _columnIndex <= columnIndex + 1; _columnIndex++)
                {
                    if (_columnIndex < 0) continue;
                    if (_columnIndex >= ColumnCount) continue;
                    if (_rowIndex == rowIndex && _columnIndex == columnIndex) continue;

                    if (tiles[_rowIndex, _columnIndex].IsMine) _surroundingMineCount++;
                }
            }
            return _surroundingMineCount;
        }

        /// <summary>
        /// 坐标周围操作
        /// </summary>
        /// <param name="rowIndex">行下标</param>
        /// <param name="columnIndex">列下标</param>
        /// <param name="callback">操作</param>
        void CycleThroughNeighbors(int rowIndex, int columnIndex, Action<int, int> callback)
        {
            int minRow = Math.Max(0, rowIndex - 1);
            int maxRow = Math.Min(RowCount - 1, rowIndex + 1);
            int minColumn = Math.Max(0, columnIndex - 1);
            int maxColumn = Math.Min(ColumnCount - 1, columnIndex + 1);

            for (int _rowIndex = minRow; _rowIndex <= maxRow; _rowIndex++)
            {
                for (int _columnIndex = minColumn; _columnIndex <= maxColumn; _columnIndex++)
                {
                    if (_rowIndex == rowIndex && _columnIndex == columnIndex) continue;
                    callback.Invoke(_rowIndex, _columnIndex);
                }
            }
        }
        #endregion
    }
    
}
