# unity perlin noise
## about
* noise exploration but mostly perlin noise in unity
* starting with white noise so we have some frame of reference
## TODO
- [ ] figure out why it looks like spaghetti >:(
- [ ] swapping between scenes
- [x] verify textures are starting from bottom left
- [x] grabbing bottom left pixel for each cell sourcing value from input noise
    * we realised we needed to check our input texture was ready before we try to use it, otherwise it might not be
- [x] simple grid uv shader
- [x] copying across white noise shaders
- [x] swapping between shaders UI
- [x] simple scene layout

## references
* the usual no. 1 [adrianb.io](https://adrianb.io/2014/08/09/perlinnoise.html)
    - their code is `C#`
* the usual no. 2 [rtouti.github.io](https://rtouti.github.io/graphics/perlin-noise-algorithm)
    - quick `C++` example then the rest is `javascript`
* Ken the man himself [An Image Synthesizer - Ken Perlin 1985](https://www.scribd.com/document/809069705/An-Image-Synthesizer-Ken-Perlin-1985)
    - also there's the newer 2002: [Improved Noise reference implementation - Ken Perlin](https://cs.nyu.edu/~perlin/noise/)
        * and [cooresponding paper](https://mrl.cs.nyu.edu/~perlin/paper445.pdf)
        * in the improved version they start using the cell uv coordinates to generate randomness rather than doing their random vector shenanigans
    - mostly `C`
* very nice explaination for unity people [jdhwilkins.com](https://jdhwilkins.com/mountains-cliffs-and-caves-a-comprehensive-guide-to-using-perlin-noise-for-procedural-generation/)
* how am i just now finding [this glorious deep dive](https://blog.pkh.me/p/42-sharing-everything-i-could-understand-about-gradient-noise.html)
    - they do get a lil mathy with their equations further down but their early breakdowns are really nice
    - it does feel like they skip around slightly for some of the visualisations