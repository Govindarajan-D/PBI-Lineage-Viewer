const path = require('path');
const HTMLWebpackPlugin = require('html-webpack-plugin');
const HtmlInlineCSSWebpackPlugin = require('html-inline-css-webpack-plugin').default;
const HtmlInlineScriptWebpackPlugin = require('html-inline-script-webpack-plugin');

module.exports = {
    context: path.resolve(__dirname, './pbi_lineage_viewer/public/'),
    entry: {
        main: './bundle.js', // Entry JavaScript file
    },
    output: {
        filename: '[name].js', // Output JS bundle
        path: path.resolve(__dirname, 'dist'), // Output directory
        clean: true, // Clean output directory before build
    },
    module: {
        rules: [
            {
                test: /\.css$/, // Match CSS files
                use: [
                    'style-loader', // Extract CSS into separate files
                    'css-loader', // Resolve CSS imports
                ],
            },
        ],
    },
    plugins: [
        new HTMLWebpackPlugin({
            template: './index.html', // Path to your index.html
            inject: 'body', // Inject scripts into the body

            minify: {
                collapseWhitespace: true, // Minify HTML
                removeComments: true, // Remove comments
            },
        }),
        new HtmlInlineCSSWebpackPlugin(), // Inline CSS into <style> tags
        new HtmlInlineScriptWebpackPlugin(), // Inline JavaScript into <script> tags
    ],
    optimization: {
        minimize: true,
        minimizer: [
            new (require('terser-webpack-plugin'))({
                extractComments: false, // Remove comments
            }),
        ],
    },
    mode: 'production',
};
