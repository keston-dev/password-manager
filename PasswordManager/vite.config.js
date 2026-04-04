export default { 
    build: { 
        outDir: 'wwwroot/dist', 
        rollupOptions: {
            input: 'src/main.js', 
            output: { 
                entryFileNames: 'bundle.js'
            }
        }
    },
    optimizeDeps: {
        include: [
            '@zxcvbn-ts/core',
            '@zxcvbn-ts/language-common',
            '@zxcvbn-ts/language-en'
        ]
    }
}