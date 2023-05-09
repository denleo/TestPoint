import path from "path";

import CopyWebpackPlugin from "copy-webpack-plugin";
import HtmlWebpackPlugin from "html-webpack-plugin";
import TsconfigPathsPlugin from "tsconfig-paths-webpack-plugin";
import { Configuration as WebpackConfiguration, HotModuleReplacementPlugin } from "webpack";
import { Configuration as WebpackDevServerConfiguration } from "webpack-dev-server";

interface Configuration extends WebpackConfiguration {
  devServer?: WebpackDevServerConfiguration;
}

const src = path.join(__dirname, "src");
const dist = path.join(__dirname, "dist");

const config: Configuration = {
  mode: "development",
  output: {
    publicPath: "/",
  },
  entry: "./src/index.tsx",
  module: {
    rules: [
      {
        test: /\.(ts|js)x?$/i,
        exclude: /node_modules/,
        use: {
          loader: "babel-loader",
          options: {
            presets: ["@babel/preset-env", "@babel/preset-react", "@babel/preset-typescript"],
          },
        },
      },
      {
        test: /\.(png|svg|jpg|gif)$/, // определяем типы файлов, которые будем обрабатывать
        use: [
          {
            loader: "url-loader",
            options: {
              limit: 8192, // если размер файла меньше 8 КБ, то он будет встроен в CSS-файл в формате base64
              fallback: "file-loader", // если размер файла больше 8 КБ, то используется file-loader для сохранения файла на диск
              outputPath: "images", // указываем путь для сохранения файлов изображений
            },
          },
        ],
      },
    ],
  },
  resolve: {
    extensions: [".tsx", ".ts", ".js"],
    alias: {
      "@": path.resolve(__dirname, "src"),
      "@components": path.resolve(__dirname, "src/components"),
      "@containers": path.resolve(__dirname, "src/containers"),
      "@layout": path.resolve(__dirname, "src/layout"),
      "@constants": path.resolve(__dirname, "src/constants"),
      "@common": path.resolve(__dirname, "src/common"),
      "@api": path.resolve(__dirname, "src/api"),
      "@redux": path.resolve(__dirname, "src/redux"),
    },
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: path.join(src, "index.html"),
    }),
    new HotModuleReplacementPlugin(),
    // new TsconfigPathsPlugin(),
    new CopyWebpackPlugin({
      patterns: ["common/favicon", "common/styles", "shared"].map((e: any) => ({
        from: path.join(src, e.from || e),
        to: path.join(dist, e.to || e),
      })),
    }),
  ],
  devtool: "inline-source-map",
  devServer: {
    static: path.join(__dirname, "dist"),
    historyApiFallback: true,
    port: 3000,
    open: true,
    hot: true,
    // hot: false,
  },
};

export default config;
