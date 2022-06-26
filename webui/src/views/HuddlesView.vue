<script setup lang="ts">
import moment from 'moment';

import type { FetchHuddlesResponse } from '@/models/responses/fetch-huddles-response';
import { useAuthenticationStore } from '@/stores/authentication';

const authStore = useAuthenticationStore();

const date = moment().format('yyyy-MM-DD');
const offset = new Date().getTimezoneOffset();

const response = await authStore.get('/api/huddle', {});
const huddles = (await response.json()) as FetchHuddlesResponse[];
</script>

<template>
  <span> {{ huddles?.length }}</span>
</template>
