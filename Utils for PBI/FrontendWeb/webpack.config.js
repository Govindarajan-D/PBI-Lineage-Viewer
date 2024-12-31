const path = require('path');
const HtmlBundlerPlugin = require('html-bundler-webpack-plugin');

module.exports = {
    context: path.resolve(__dirname, './pbi_lineage_viewer/public/'),
    output: {
        path: path.resolve(__dirname, 'dist'), // Output directory
        clean: true, // Clean output directory before build
    },
    module: {
        rules: [
            {
                test: /\.css$/, // Match CSS files
                use: [
                    'css-loader', // Resolve CSS imports
                ],
            },
        ],
    },
    plugins: [
        new HtmlBundlerPlugin({
          entry: {
            index: './index.html', // Path to your index.html
          },
          js: {
            // output filename of compiled JavaScript, used if inline is false
            filename: '[name].[contenthash:8].js',
            inline: true, // Inline JavaScript into <script> tags
          },
          css: {
            // output filename of extracted CSS, used if inline is false
            filename: '[name].[contenthash:8].css',
            inline: true, // Inline CSS into <style> tags
          },
          minify: true, // Enable minify HTML
          minifyOptions: {
            collapseWhitespace: true, // Minify HTML
            removeComments: true, // Remove comments
          },
        }),
    ],
    mode: 'production',
};