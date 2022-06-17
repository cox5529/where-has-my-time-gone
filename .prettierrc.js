module.exports = {
  trailingComma: 'es5',
  semi: true,
  singleQuote: true,
  jsxSingleQuote: true,
  importOrder: ['^react$', '^[^\\.]*$', '^((\\./)|(\\.\\./))+[^\\.]+$', '^\\..*.css$', '^(\\./)[^\\.]+.*$', '^.*$'],
  importOrderSeparation: true,
  importOrderParserPlugins: [
    "typescript",
    "[\"decorators\", { \"decoratorsBeforeExport\": true }]"
  ]
};
