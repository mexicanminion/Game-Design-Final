using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Hostile_Backpack_Angry_Travel
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HostileAngryBackpack : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Matrix matrix;

        Texture2D playerimg, backimg, floorImg, pageImg, spriteSheetHearts, groundEnemySS, beginButton, backButton, infoButton, exitButton, emptyPixel, logo;

        float scaleX, scaleY;

        public static int SPRITE_EXPECTED_WIDTH = 480;
        public static int SPRITE_EXPECTED_HEIGHT = 360;

        const int MAX_HEATH_SPOTS = 3;
        int currLives = 3;
        int score = 0;
        int loop = 0;

        Vector2 scoreVector;

        ScreenState screen = ScreenState.intro;

        SpriteFont minecraftia12, minecraftia36;

        Song introTheme, playingTheme;

        Player player;
        Background_and_Floor backAndFloor;
        BarrierList barrierList = new BarrierList();
        GoundEnemyList groundEnemyList;
        JetpackFailure fuel;
        GUI gui;

        KeyboardState keyboardState;
        KeyboardState oldkeyboardState;
        GamePadState gamepad1;
        GamePadState prevGamepad1;
        MouseState mouse;
        MouseState prevMouse;

        Lives[] lives = new Lives[MAX_HEATH_SPOTS];


        public HostileAngryBackpack()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            barrierList.initBarrierList();
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerimg = Content.Load<Texture2D>("player");
            backimg = Content.Load<Texture2D>("tempBack");
            floorImg = Content.Load<Texture2D>("tempFloor");
            pageImg = Content.Load<Texture2D>("SinglePage");
            spriteSheetHearts = Content.Load<Texture2D>("hearts");
            groundEnemySS = Content.Load<Texture2D>("groundEnemySS");
            minecraftia12 = Content.Load<SpriteFont>("Minecraftia12");
            minecraftia36 = Content.Load<SpriteFont>("Minecraftia36");
            beginButton = Content.Load<Texture2D>("START");
            exitButton = Content.Load<Texture2D>("EXIT");
            infoButton = Content.Load<Texture2D>("INFO");
            backButton = Content.Load<Texture2D>("BACK");
            logo = Content.Load<Texture2D>("PaperPac Attack Logo");
            introTheme = Content.Load<Song>("The Good Fight (w intro)");
            playingTheme= Content.Load<Song>("Battle Theme II v1.2");
            emptyPixel = Content.Load<Texture2D>("pixel");


            groundEnemyList = new GoundEnemyList(groundEnemySS);
            groundEnemyList.iniEnemyList();

            player = new Player(playerimg, pageImg,new Rectangle(100, SPRITE_EXPECTED_HEIGHT, 24, 36), 5);
            backAndFloor = new Background_and_Floor(backimg, floorImg);
            barrierList.createBarrierList(Content, 20, 30, SPRITE_EXPECTED_HEIGHT);
            groundEnemyList.createEnemyList(Content, 10, SPRITE_EXPECTED_HEIGHT);

            fuel = new JetpackFailure(100, 2, 1.5);
            fuel.LoadContent(Content);

            gui = new GUI(beginButton, backButton, infoButton, exitButton);

            player.createparticles(GraphicsDevice);
            fillLives();
        }

        private void fillLives()
        {
            for (int i = 0; i < MAX_HEATH_SPOTS; i++)
            {
                lives[i] = new Lives(spriteSheetHearts, new Rectangle((10 * i) + (5* i +10), SPRITE_EXPECTED_HEIGHT - 20,10,10));
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            gamepad1 = GamePad.GetState(PlayerIndex.One);
            keyboardState = Keyboard.GetState();
            mouse = Mouse.GetState();

            scaleX = (float)GraphicsDevice.Viewport.Width / SPRITE_EXPECTED_WIDTH;
            scaleY = (float)GraphicsDevice.Viewport.Height / SPRITE_EXPECTED_HEIGHT;
            matrix = Matrix.CreateScale(scaleX, scaleY, 1.0f);
            gui.updateScale(scaleX, scaleY);

            backAndFloor.updatePos(gameTime);
            gui.updateGamePad(gamepad1, prevGamepad1, gamepad1.IsConnected);
            

            switch (screen){
                case ScreenState.intro:
                    updateIntro();
                    break;
                case ScreenState.info:
                    updateInfo();
                    break;
                case ScreenState.playing:
                    updatePlaying(gameTime);
                    break;
                case ScreenState.ending:
                    updateEnding();
                    break;
            }

            checkDeath();

            oldkeyboardState = keyboardState;
            prevGamepad1 = gamepad1;
            prevMouse = mouse;
            base.Update(gameTime);
        }

        public void checkDeath()
        {
            if(currLives == 0)
            {
                screen = ScreenState.ending;
                currLives = 3;
                barrierList.purgeAll();
                groundEnemyList.purgeAll();
                player.reset();
                fuel.reset();
                fillLives();
            }
        }

        private void updateIntro()
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(introTheme);
            }

            if (gui.updatebegin(mouse, prevMouse))
            {
                screen = ScreenState.playing;
                MediaPlayer.Stop();
            }

            if (gui.updateInfo(mouse, prevMouse))
            {
                screen = ScreenState.info;
            }

            if (gui.updateExit(mouse, prevMouse))
            {
                this.Exit();
            }
        }

        private void updateInfo()
        {
            if (gui.updateBack(mouse, prevMouse))
            {
                screen = ScreenState.intro;
            }
        }

        private void updatePlaying(GameTime gt)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(playingTheme);
            } 

            player.processInput(keyboardState, gamepad1, fuel.fuelOut());
            if (barrierList.updateBarriers(gt, player.getRect(), Content) || groundEnemyList.updateEnemies(gt, player.getRect(), Content))
            {
                lives[currLives - 1].decreaseLives();
                currLives--;
            }
            else
            {
                if (loop % 5 == 0)
                {
                    score++;
                    fuel.updateFuel(keyboardState, gamepad1);
                    loop = 0;
                }

            }

            loop++;
        }

        private void updateEnding()
        {
            if (gui.updateBack(mouse, prevMouse))
            {
                score = 0;
                screen = ScreenState.intro;
                MediaPlayer.Stop();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: matrix);

            backAndFloor.Draw(spriteBatch);

            switch (screen)
            {
                case ScreenState.intro:
                    drawIntro();
                    break;
                case ScreenState.info:
                    drawInfo();
                    break;
                case ScreenState.playing:
                    drawPlaying();
                    break;
                case ScreenState.ending:
                    drawEnding();
                    break;
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void drawIntro()
        {
            spriteBatch.Draw(logo, new Rectangle((SPRITE_EXPECTED_WIDTH/2) - 185, 25, 375, 170), Color.White);

            gui.drawBegin(spriteBatch);
            gui.drawInfo(spriteBatch);
            gui.drawExit(spriteBatch);
        }

        private void drawInfo()
        {
            string info = "INFO";
            string fly = "Hold SPACE to fly!";
            string objective = "You need to dodge the pencils \n    and teachers in the halls!";
            string objective2 = "The longer you survive\nthe more points you get!";
            string objective3 = "But watch out!";
            string objective4 = "You only have 3 lives and little paper!";


            Vector2 infoV = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia36.MeasureString(info).X / 2, 50);
            Vector2 flyV = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia12.MeasureString(fly).X / 2, 125);
            Vector2 objectiveV = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia12.MeasureString(objective).X / 2, 150);
            Vector2 objective2V = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia12.MeasureString(objective2).X / 2, 200);
            Vector2 objective3V = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia12.MeasureString(objective3).X / 2, 250);
            Vector2 objective4V = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia12.MeasureString(objective4).X / 2, 275);

            spriteBatch.Draw(emptyPixel, new Rectangle(50,50, 375, 250), Color.White);
            spriteBatch.DrawString(minecraftia36, info, infoV, Color.Black);
            spriteBatch.DrawString(minecraftia12, fly, flyV, Color.Black);
            spriteBatch.DrawString(minecraftia12, objective, objectiveV, Color.Black);
            spriteBatch.DrawString(minecraftia12, objective2, objective2V, Color.Black);
            spriteBatch.DrawString(minecraftia12, objective3, objective3V, Color.Black);
            spriteBatch.DrawString(minecraftia12, objective4, objective4V, Color.Black);

            gui.drawBack(spriteBatch);

        }

        private void drawPlaying()
        {
            barrierList.draw(spriteBatch);
            groundEnemyList.draw(spriteBatch);
            player.Draw(spriteBatch);
            for (int i = 0; i < MAX_HEATH_SPOTS; i++)
            {
                lives[i].draw(spriteBatch);
            }
            fuel.draw(spriteBatch);
            string currScoreString = score.ToString();
            scoreVector = new Vector2(10, 10); //GraphicsDevice.Viewport.Height / 1.75f - minecraftia24.MeasureString(currScoreString).Y / 2); //set y
            spriteBatch.DrawString(minecraftia12, currScoreString, scoreVector, Color.Black);
        }

        private void drawEnding()
        {
            string currScoreString = "Game Over!";
            string scoreMax = "Your high socre was " + score.ToString() + " !";
            scoreVector = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia36.MeasureString(currScoreString).X /2 , 100); //GraphicsDevice.Viewport.Height / 1.75f - minecraftia24.MeasureString(currScoreString).Y / 2); //set y
            Vector2 maxscore = new Vector2(SPRITE_EXPECTED_WIDTH / 2 - minecraftia12.MeasureString(scoreMax).X / 2, 200);
            spriteBatch.Draw(emptyPixel, new Rectangle(50, 50, 375, 250), Color.White);
            spriteBatch.DrawString(minecraftia36, currScoreString, scoreVector, Color.Black);
            spriteBatch.DrawString(minecraftia12, scoreMax, maxscore, Color.Black);
            gui.drawBack(spriteBatch);
        }

    }

    enum ScreenState
    {
        intro,
        info,
        playing,
        ending
    }
}
