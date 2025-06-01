import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import { viteSingleFile } from 'vite-plugin-singlefile'

export default defineConfig({
  plugins: [
    svelte(),
    viteSingleFile(), // Add this plugin to bundle everything into a single file
  ],
  build: {
    target: 'esnext', // Ensure modern JavaScript is used
    assetsInlineLimit: Infinity, // Inline all assets
    cssCodeSplit: false, // Prevent CSS from being split into separate files
  },
})