const path = require('path');
const HTMLWebpackPlugin = require('html-webpack-plugin');
const HTMLInlineScriptPlugin = require('html-inline-script-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
    entry: {
        main: './src/js/index.js',
    },
    output: {
        filename: '[name].js',
        path: path.resolve(__dirname, 'dist'),
        clean: true,
    },
    module: {
        rules: [
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader']

            },
        ],
    },
    plugins: [
        new HTMLWebpackPlugin({
            template: './src/index.html',
            inject: 'body',
            chunks: ['main'],
        }),
        new HTMLInlineScriptPlugin(),
        new CopyWebpackPlugin({
            patterns: [
                { from: 'src/js/cytoscape.min.js', to: 'js/cytoscape.min.js' },
                { from: 'src/js/cytoscape-dagre.js', to: 'js/cytoscape-dagre.js' },
                { from: 'src/js/dagre.js', to: 'js/dagre.js' },
            ],
        }),
    ],
    mode: 'production',
}