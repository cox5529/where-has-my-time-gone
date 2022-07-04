<script setup lang="ts">
import type { Huddle } from './HuddleDataGridRow.vue';

defineProps<{ huddle: Huddle }>();
</script>

<template>
  <div
    class="bg-blue-500 absolute h-12 translate-y-12"
    :style="{ left: `${huddle.left}%`, width: `${huddle.width}%` }"
  ></div>
</template>
