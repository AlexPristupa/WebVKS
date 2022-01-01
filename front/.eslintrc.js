module.exports = {
  root: true,
  parserOptions: {
    parser: '@typescript-eslint/parser',
    ecmaVersion: 2020,
    // sourceType: 'module',
  },
  env: {
    browser: true,
    node: true,
    es6: true,
  },
  extends: [
    'plugin:vue/essential',
    'eslint:recommended',
    '@vue/typescript/recommended',
    '@vue/prettier',
    '@vue/prettier/@typescript-eslint',
  ],
  rules: {
    'vue/max-attributes-per-line': [
      2,
      {
        singleline: 10,
        multiline: {
          max: 1,
          allowFirstLine: false,
        },
      },
    ],

    'vue/singleline-html-element-content-newline': 'error',
    'vue/multiline-html-element-content-newline': 'error',
    'vue/name-property-casing': ['error', 'PascalCase'],
    'vue/no-v-html': 'off',
    'accessor-pairs': 'error',
    'arrow-spacing': [
      'error',
      {
        before: true,
        after: true,
      },
    ],
    'block-spacing': ['error', 'always'],
    'brace-style': [
      'error',
      '1tbs',
      {
        allowSingleLine: true,
      },
    ],
    'comma-dangle': [
      'warn',
      {
        arrays: 'always-multiline',
        objects: 'always-multiline',
        imports: 'always-multiline',
        exports: 'always-multiline',
        functions: 'only-multiline',
      },
    ],
    'comma-spacing': [
      'warn',
      {
        before: false,
        after: true,
      },
    ],
    'comma-style': ['error', 'last'],
    'constructor-super': 'error',
    curly: ['error', 'multi-line'],
    'dot-location': ['error', 'property'],
    'eol-last': ['error', 'always'],
    eqeqeq: ['error', 'always', { null: 'ignore' }],
    'generator-star-spacing': [
      'error',
      {
        before: false,
        after: true,
      },
    ],

    //'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-console': 'off',
    quotes: [
      'warn',
      'single',
      { allowTemplateLiterals: true, avoidEscape: true },
    ],
    semi: [
      'warn',
      'never',
      {
        beforeStatementContinuationChars: 'any',
      },
    ],
    'max-statements-per-line': ['warn', { max: 1 }],
    'no-sparse-arrays': 'error',
    'array-bracket-newline': ['error', 'consistent'],
    'array-element-newline': ['error', 'consistent'],
    '@typescript-eslint/interface-name-prefix': 'off',
    '@typescript-eslint/no-var-requires': 0,
    '@typescript-eslint/camelcase': 'off',
    '@typescript-eslint/class-name-casing': 'off',
    '@typescript-eslint/no-empty-interface': 'off',
    '@typescript-eslint/no-unused-vars': 'off',
    '@typescript-eslint/no-inferrable-types': 'off',
    'no-empty-function': 'off',
    '@typescript-eslint/no-empty-function': 'off',
    '@typescript-eslint/no-explicit-any': 'off',
    '@typescript-eslint/consistent-type-assertions': 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 2 : 0,
    '@typescript-eslint/ban-ts-ignore': 'off',
  },
}
