using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DrawTriangle
{
	/// <summary>
	/// ゲームメインクラス
	/// </summary>
	public class Game1 : Game
	{
    /// <summary>
    /// グラフィックデバイス管理クラス
    /// </summary>
    private readonly GraphicsDeviceManager _graphics = null;

    /// <summary>
    /// スプライトのバッチ化クラス
    /// </summary>
    private SpriteBatch _spriteBatch = null;

    /// <summary>
    /// 頂点データリスト
    /// </summary>
    private VertexPositionColor[] _vertices = null;

    /// <summary>
    /// 基本エフェクト
    /// </summary>
    private BasicEffect _basicEffect = null;


    /// <summary>
    /// GameMain コンストラクタ
    /// </summary>
    public Game1()
    {
      // グラフィックデバイス管理クラスの作成
      _graphics = new GraphicsDeviceManager(this);

      // ゲームコンテンツのルートディレクトリを設定
      Content.RootDirectory = "Content";

      // マウスカーソルを表示
      IsMouseVisible = true;
    }

    /// <summary>
    /// ゲームが始まる前の初期化処理を行うメソッド
    /// グラフィック以外のデータの読み込み、コンポーネントの初期化を行う
    /// </summary>
    protected override void Initialize()
    {
      // TODO: ここに初期化ロジックを書いてください

      // コンポーネントの初期化などを行います
      base.Initialize();
    }

    /// <summary>
    /// ゲームが始まるときに一回だけ呼ばれ
    /// すべてのゲームコンテンツを読み込みます
    /// </summary>
    protected override void LoadContent()
    {
      // テクスチャーを描画するためのスプライトバッチクラスを作成します
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      // エフェクトを作成
      _basicEffect = new BasicEffect(GraphicsDevice);

      // エフェクトで頂点カラーを有効にする
      _basicEffect.VertexColorEnabled = true;

      // ビューマトリックスをあらかじめ設定 ((0, 0, 15) から原点を見る)
      _basicEffect.View = Matrix.CreateLookAt(
              new Vector3(0.0f, 0.0f, 15.0f),
              Vector3.Zero,
              Vector3.Up
          );

      // プロジェクションマトリックスをあらかじめ設定
      _basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
              MathHelper.ToRadians(45.0f),
              (float)GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height,
              1.0f,
              100.0f
          );

      // 頂点データを作成する
      _vertices = new VertexPositionColor[3];

      _vertices[0] = new VertexPositionColor(new Vector3(0.0f, 3.0f, 0.0f),
                                                 Color.Red);
      _vertices[1] = new VertexPositionColor(new Vector3(3.0f, -2.0f, 0.0f),
                                                 Color.Blue);
      _vertices[2] = new VertexPositionColor(new Vector3(-3.0f, -2.0f, 0.0f),
                                                 Color.Green);
    }

    /// <summary>
    /// ゲームが終了するときに一回だけ呼ばれ
    /// すべてのゲームコンテンツをアンロードします
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: ContentManager で管理されていないコンテンツを
      //       ここでアンロードしてください
    }

    /// <summary>
    /// 描画以外のデータ更新等の処理を行うメソッド
    /// 主に入力処理、衝突判定などの物理計算、オーディオの再生など
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Update(GameTime gameTime)
    {
      // ゲームパッドの Back ボタン、またはキーボードの Esc キーを押したときにゲームを終了させます
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      // TODO: ここに更新処理を記述してください

      // 登録された GameComponent を更新する
      base.Update(gameTime);
    }

    /// <summary>
    /// 描画処理を行うメソッド
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Draw(GameTime gameTime)
    {
      // 画面を指定した色でクリアします
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // パスの数だけ繰り替えし描画 (といっても直接作成した BasicEffect は通常１回)
      foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
      {
        // パスの開始
        pass.Apply();

        // ポリゴンを描画する
        GraphicsDevice.DrawUserPrimitives(
            PrimitiveType.TriangleList,
            _vertices,
            0,
            1
        );
      }

      // 登録された DrawableGameComponent を描画する
      base.Draw(gameTime);
    }
  }
}
