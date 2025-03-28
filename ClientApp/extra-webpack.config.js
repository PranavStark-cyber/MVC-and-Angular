
const path = require('path');
var rmdir = require('rmdir');
const mvcAppPath = path.resolve(
  __dirname,
  "../MVCTest"
);
var distPath=path.join(mvcAppPath, "/wwwroot/dist");
rmdir(distPath);
module.exports = {
  experiments: {
    topLevelAwait: true
  },
  entry: {
    "main": [
      "./src\\main.ts"
    ],
    "polyfills": [
      "./src\\polyfills.ts"
    ]
  },
  /* resolve: {
    extensions: ['.ts', '.js'],
  },

 output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js',
  },

  module: {
    rules: [
      {
        test: /\.ts$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
      {
        test: /\.scss$/,
        exclude: /node_modules/,
        use: ['style-loader', 'css-loader', 'sass-loader'],
      },
    ],
  },
  devServer: {
    contentBase: path.join(__dirname, 'dist'),
    port: 4200,
  },*/
  "output": {
    "path": distPath,
    "publicPath": "../dist/",
    "filename": "[name].[contenthash].bundle.js",
    //"filename": (chunkData) => {
    //    return chunkData.chunk.name + ".bundle.js";
    //},
    //"chunkFilename": "[id].chunk.js",  .[hash]
    "chunkFilename": "[name].[contenthash].chunk.js",
    "crossOriginLoading": false
},
};
