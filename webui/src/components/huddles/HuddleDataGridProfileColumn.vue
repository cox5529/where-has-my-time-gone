<script setup lang="ts">
import HuddleDataGridProfileColumnEntry from './HuddleDataGridProfileColumnEntry.vue';

import type { FetchHuddlesProfileResponse } from '@/models/responses/fetch-huddles-response';

defineProps<{
  profiles: FetchHuddlesProfileResponse[];
}>();
</script>

<template>
  <div
    class="sticky bg-slate-900 left-0 top-0 w-32 border-r-4 flex-shrink-0 z-10"
  >
    <div
      class="h-24 flex-shrink-0 border-b-4 sticky top-0 bg-slate-900 z-10"
    ></div>
    <HuddleDataGridProfileColumnEntry
      v-for="profile in profiles"
      :key="profile.userProfileId"
      :profile="profile"
    />
  </div>
</template>
