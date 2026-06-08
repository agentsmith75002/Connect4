namespace Connect4

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open System
open Microsoft.Xna.Framework.Input

type Connect4App(nbRows:int, nbColumns: int) as this =
    inherit Game()

    let SQUARE_SIZE = 100
    let OFFSET = 5

    let mutable nbRow: int = 0
    let mutable nbCol: int = 0
    let mutable square = Unchecked.defaultof<Texture2D>
    let mutable circle = Unchecked.defaultof<Texture2D>
    let mutable graphicManager: GraphicsDeviceManager = new GraphicsDeviceManager(this)
    let mutable spriteBatch: SpriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable prevMouseState = Mouse.GetState()
    
    let createCircle radius color =
        let diameter = radius * 2
        let texture = new Texture2D(this.GraphicsDevice, diameter, diameter)
        let colorData = Array.create (diameter * diameter) Color.Transparent

        for x in 0 .. diameter - 1 do
            for y in 0 .. diameter - 1 do
                let dx = radius - x
                let dy = radius - y

                if dx * dx + dy * dy <= radius * radius then
                    colorData.[y * diameter + x] <- color

        texture.SetData(colorData)
        texture
    
    let createSquare size color =
        let texture = new Texture2D(this.GraphicsDevice, size, size)
        let colorData = Array.create (size * size) color
        texture.SetData(colorData)
        texture

    do 
            nbRow <- nbRows
            nbCol <- nbColumns
            //this.Initialize()
    with

        override this.Initialize() =
            this.IsMouseVisible <- true
            // this.IsFixedTimeStep <- true
            this.Window.Title <- "Connect 4"
            this.Window.AllowAltF4 <- true
            base.Initialize()

        override this.LoadContent() =
            graphicManager.PreferredBackBufferWidth <- nbCol * SQUARE_SIZE
            graphicManager.PreferredBackBufferHeight <- (nbRow + 1) * SQUARE_SIZE

            graphicManager.ApplyChanges()

            spriteBatch <- new SpriteBatch(this.GraphicsDevice)

            square <- createSquare SQUARE_SIZE Color.Blue
            circle <- createCircle (SQUARE_SIZE/2-OFFSET) Color.White

        override this.Draw(gameTime: GameTime) =
            this.GraphicsDevice.Clear(Color.Black)

            spriteBatch.Begin()
            for c in 0 .. nbCol - 1 do
                for r in 0 .. nbRow - 1 do
                    let x = c*SQUARE_SIZE
                    let y = r*SQUARE_SIZE+SQUARE_SIZE
                    //printfn "x:%d y:%d offset:%d" x y offset
                    spriteBatch.Draw(square, new Vector2(float32 (x), float32 (y)), Color.White)
                    spriteBatch.Draw(circle, new Vector2(float32 (x + OFFSET), float32 (y + OFFSET)), Color.Black)
            spriteBatch.End()
            
            base.Draw(gameTime)

        override this.Update(gameTime: GameTime) =
            let mouseState = Mouse.GetState()

            if mouseState.LeftButton = ButtonState.Pressed && prevMouseState.LeftButton = ButtonState.Released then
                let mouseX = mouseState.X
                let mouseY = mouseState.Y
                printfn "Mouse down at (%d, %d)" mouseX mouseY

            prevMouseState <- mouseState

            base.Update(gameTime)
            
        override this.OnExiting(sender: obj, args: EventArgs) =
            base.OnExiting(sender, args)
            this.Exit()
