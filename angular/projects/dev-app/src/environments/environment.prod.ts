import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44309/',
  redirectUri: baseUrl,
  clientId: 'WebPush_App',
  responseType: 'code',
  scope: 'offline_access WebPush',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'WebPush',
    logoUrl: '',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44309',
      rootNamespace: 'DecisionTree.Abp.Notification.WebPush',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
    WebPush: {
      url: 'https://localhost:44389',
      rootNamespace: 'DecisionTree.Abp.Notification.WebPush',
    },
  },
} as Environment;
